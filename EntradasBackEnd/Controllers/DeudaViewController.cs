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

            Deudas deudas = new Deudas();

            deudas.CalculateDebts(balanceViewL);            

            return deudas.deudas;
        }        
    }
}
