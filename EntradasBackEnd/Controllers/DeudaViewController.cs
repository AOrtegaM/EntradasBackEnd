using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntradasBackEnd;
using EntradasBackEnd.Data;

namespace EntradasBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeudaViewController : ControllerBase
    {
        private readonly EntradasBackEndContext _context;

        public DeudaViewController(EntradasBackEndContext context)
        {
            _context = context;
        }

        // GET: api/DeudaView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deuda>>> GetDeudaView()
        {
            var balanceViewL = await _context.BalanceView.ToListAsync();

            /* List<BalanceView> balanceViewL = [];

            balanceViewL.Add(new BalanceView(1,"Persona1", "", 2.00m));
            balanceViewL.Add(new BalanceView(2,"Persona2", "", 1.75m));
            balanceViewL.Add(new BalanceView(3,"Persona3", "", 1.50m));
            balanceViewL.Add(new BalanceView(4,"Persona4", "", 1.25m));
            balanceViewL.Add(new BalanceView(5,"Persona5", "", 1.00m));
            balanceViewL.Add(new BalanceView(6,"Persona6", "", 1.00m));
            balanceViewL.Add(new BalanceView(7,"Persona7", "", 0.75m));
            balanceViewL.Add(new BalanceView(8,"Persona8", "", 0.50m));
            balanceViewL.Add(new BalanceView(9,"Persona9", "", 0.25m));
            balanceViewL.Add(new BalanceView(10, "Persona10", "", 0.00m));
            */

            Deudas deudas = new Deudas();

            deudas.CalculateDebts(balanceViewL);            

            return deudas.deudas;
        }        
    }
}
