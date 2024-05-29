using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;

namespace EntradasBackEnd
{
    public class Deudas
    {
        public List<Deuda> deudas = [];

        static bool CompareBitArrays(BitArray x, BitArray y)
        {
            if (x.Length != y.Length) return false;

            int[] xInts = new int[(int)Math.Ceiling((decimal)x.Count / 32)];
            int[] yInts = new int[(int)Math.Ceiling((decimal)y.Count / 32)];

            x.CopyTo(xInts, 0);
            y.CopyTo(yInts, 0);

            bool areDiff = false;

            for (int i = 0; i < xInts.Length && areDiff == false; i++) areDiff = (xInts[i] != yInts[i]);

            return !areDiff;
        }
        static bool IsSimpleCompensation(BitArray bitArray, List<BitArray> compensations)
        {
            foreach (var compensation in compensations)
            {
                BitArray w = new BitArray(bitArray);
                if (CompareBitArrays(w.And(compensation), compensation) == true) return false;
            }

            return true;
        }

        static List<BalanceView> ClearZerosInBalance(List<BalanceView> balance)
        {
            List<BalanceView> newList = new List<BalanceView>(balance);            

            newList.RemoveAll(i => i.importe == 0);

            return newList;
        }

        static List<BitArray> GetSimpleCompensations(List<BalanceView> balance)
        {
            BitArray bitArray;
            List<BitArray> compensations = [];
            decimal sum;

            int max = (int)Math.Pow(2, balance.Count);

            for (int i = 1; i < max; i++)
            {
                bitArray = new BitArray(BitConverter.GetBytes(i));
                sum = 0;
                for (int j = 0; j < balance.Count; j++)
                {
                    if (bitArray[j] == true) sum += balance[j].importe;
                }
                if (sum == 0)
                {
                    if (IsSimpleCompensation(bitArray, compensations) == true) {
                        compensations.Add(bitArray);                        
                    }
                }
            }

            return compensations;
        }

        static BitArray GetOptimalCompesationsBitArray(List<BitArray> compensations)
        {
            int zero = 0;
            int solution_index = 0;
            int optimal = 0;

            BitArray bitArray;

            int max = (int)Math.Pow(2, compensations.Count);

            for (int i = 1; i < max; i++)
            {
                bitArray = new BitArray(BitConverter.GetBytes(i));
                BitArray bArrayAnd = new BitArray(BitConverter.GetBytes(zero));
                BitArray bArrayOr = new BitArray(BitConverter.GetBytes(zero));

                int subsets = 0;
                bool solution = true;

                for (int j = 0; j < compensations.Count; j++)
                {
                    if (bitArray[j] == true)
                    {
                        bArrayAnd = new BitArray(bArrayOr);

                        if (bArrayAnd.And(compensations[j]).HasAnySet() == false)
                        {
                            bArrayOr.Or(compensations[j]);
                            subsets++;
                        }
                        else
                        {
                            subsets = 0;
                            solution = false;
                            break;
                        }
                    }
                }

                if (solution == true)
                {
                    if (subsets > optimal)
                    {
                        // Nueva solucion
                        optimal = subsets;
                        solution_index = i;
                    }
                }

            }

            return new BitArray(BitConverter.GetBytes(solution_index));
        }
        static List<List<BalanceView>> GetOptimalCompesationsSets(BitArray bSolution, List<BitArray> compensations, List<BalanceView> balance)
        {
            List<List<BalanceView>> compensationsSets = [];

            for (int i = 0; i < compensations.Count; i++)
            {
                if (bSolution[i] == true)
                {
                    BitArray bitArrayC = new BitArray(compensations[i]);
                    List<BalanceView> list = [];

                    for (int j = 0; j < balance.Count; j++)
                    {
                        if (bitArrayC[j] == true)
                        {                            
                            list.Add(balance[j]);
                        }
                    }
                    compensationsSets.Add(list);                    
                }


            }
            return compensationsSets;
        }
        private void BuildOneBlock(List<BalanceView> block)
        {
            int i = 0;
            int j = block.Count - 1;
            Deuda deuda;

            while (i < j)
            {

                if (-block[i].importe > block[j].importe)
                {
                    // Debe mas dinero que lo que le deben al correspondiente.
                    deuda = new Deuda(block[i].nombre + " " + block[i].apellidos, block[j].nombre + " " + block[j].apellidos, block[j].importe);                    
                    block[i].importe += block[j].importe;
                    block[j].importe = 0;
                    j--;
                }
                else if (-block[i].importe < block[j].importe)
                {                    
                    deuda = new Deuda(block[i].nombre + " " + block[i].apellidos, block[j].nombre + " " + block[j].apellidos, -block[i].importe);
                    block[j].importe += block[i].importe;
                    block[i].importe = 0;
                    i++;
                }
                else
                {                    
                    deuda = new Deuda(block[i].nombre + " " + block[i].apellidos, block[j].nombre + " " + block[j].apellidos, block[j].importe);
                    block[i].importe = 0;
                    block[j].importe = 0;
                    j--;
                    i++;
                }

                this.deudas.Add(deuda);
            }
        }

        private void BuildDebts(List<List<BalanceView>> optimalSets)
        {            
            foreach (var block in optimalSets)
            {
                BuildOneBlock(block);
            }
        }

        static List<BalanceView> CalculateBalance(List<BalanceView> initial)
        {
            List<BalanceView> balance = [];

            BalanceView person;    

            decimal total = 0.00m;
            int elements = initial.Count;
            decimal difference = 0.00m;

            foreach (var x in initial)
            {
                total += x.importe;                
            }

            foreach (var x in initial)
            {
                person = x;
                person.importe = Math.Round(x.importe - (total / elements), 2);

                balance.Add(person);
                difference += person.importe;
            }

            balance.Sort();

            // Cuadramos
            if (difference != 0) balance[balance.Count - 1].importe -= difference;

            return balance;
        }

        public void CalculateDebts(List<BalanceView> initial)
        {
            List<List<BalanceView>> optimalSets = [];

            List<BalanceView> balance = CalculateBalance(initial);
            List<BalanceView> cleanBalance = ClearZerosInBalance(balance);            

            if (cleanBalance.Count < 20)
            {
                List<BitArray> compensations = GetSimpleCompensations(cleanBalance);                

                if (compensations.Count < 20)
                {
                    BitArray bSolution = GetOptimalCompesationsBitArray(compensations);
                    optimalSets = GetOptimalCompesationsSets(bSolution, compensations, cleanBalance);
                }
                else
                {
                    optimalSets.Add(cleanBalance);
                }
            }
            else
            {
                optimalSets.Add(cleanBalance);
            }            

            BuildDebts(optimalSets);

        }


    }
}
