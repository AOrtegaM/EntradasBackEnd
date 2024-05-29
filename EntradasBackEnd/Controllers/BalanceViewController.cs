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
    public class BalanceViewController : ControllerBase
    {
        private readonly EntradasBackEndContext _context;

        public BalanceViewController(EntradasBackEndContext context)
        {
            _context = context;
        }

        // GET: api/BalanceView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalanceView>>> GetBalanceView()
        {
            var balanceViewL = await _context.BalanceView.ToListAsync();            
            decimal balance = 0;
            int elementos = 0;

            foreach (BalanceView item in balanceViewL)
            {
                balance = balance + item.importe;
                elementos++;
            }

            foreach (BalanceView item in balanceViewL)
            {
                item.importe = Math.Round(item.importe - (balance / elementos), 2);                
            }
            return balanceViewL;
        }

        // GET: api/BalanceView/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BalanceView>> GetBalanceView(int id)
        {
            var balanceView = await _context.BalanceView.FindAsync(id);

            if (balanceView == null)
            {
                return NotFound();
            }

            return balanceView;
        }       
    }
}
