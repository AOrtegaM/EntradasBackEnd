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
    public class GastosController : ControllerBase
    {
        private readonly EntradasBackEndContext _context;

        public GastosController(EntradasBackEndContext context)
        {
            _context = context;
        }

        // GET: api/Gastos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gastos>>> GetGastos()
        {
            return await _context.Gastos.ToListAsync();
        }

        // GET: api/Gastos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gastos>> GetGastos(int id)
        {
            var gastos = await _context.Gastos.FindAsync(id);

            if (gastos == null)
            {
                return NotFound();
            }

            return gastos;
        }

        // PUT: api/Gastos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGastos(int id, Gastos gastos)
        {
            if (id != gastos.id)
            {
                return BadRequest();
            }

            _context.Entry(gastos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gastos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gastos>> PostGastos(Gastos gastos)
        {
            _context.Gastos.Add(gastos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGastos", new { id = gastos.id }, gastos);
        }

        // DELETE: api/Gastos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGastos(int id)
        {
            var gastos = await _context.Gastos.FindAsync(id);
            if (gastos == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gastos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GastosExists(int id)
        {
            return _context.Gastos.Any(e => e.id == id);
        }
    }
}
