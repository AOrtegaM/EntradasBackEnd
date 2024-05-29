using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntradasBackEnd
{
    public class BalanceView : IComparable<BalanceView>
    {
        public int id { get; set; }         
        public string? nombre { get; set; }
        public string? apellidos { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal importe { get; set; }

        public BalanceView(int id, string nombre, string apellidos,  decimal importe)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.importe = importe;
        }


        public int CompareTo(BalanceView ?other)
        {
            if ( other == null ) return 1;
            if (this.importe < other.importe) return -1;
            else if (this.importe > other.importe) return 1;
            else return 0;
        }
    }

    

}
