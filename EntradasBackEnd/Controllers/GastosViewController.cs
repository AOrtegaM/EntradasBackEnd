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
    public class GastosViewController : ControllerBase
    {
        private readonly EntradasBackEndContext _context;

        public GastosViewController(EntradasBackEndContext context)
        {
            _context = context;
        }

        // GET: api/GastosView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GastosView>>> GetGastosView()
        {
            return await _context.GastosView.ToListAsync();
        }

        // GET: api/GastosView/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GastosView>> GetGastosView(int id)
        {
            var gastosView = await _context.GastosView.FindAsync(id);

            if (gastosView == null)
            {
                return NotFound();
            }

            return gastosView;
        }        
    }
}
