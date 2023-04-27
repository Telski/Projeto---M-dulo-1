using System.Collections.Generic;
using System.Threading.Tasks;
using LabSchool.Models;
using LabSchool.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedagogosController : ControllerBase
    {
        private readonly LabSchoolContext _context;

        public PedagogosController(LabSchoolContext context)
        {
            _context = context;
        }

        // GET: api/pedagogos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedagogo>>> GetPedagogos()
        {
            return Ok(await _context.Pedagogos.ToListAsync());
        }

        // GET: api/pedagogos/{codigo}
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Pedagogo>> GetPedagogo(int codigo)
        {
            var pedagogo = await _context.Pedagogos.FindAsync(codigo);

            return pedagogo != null ? Ok(pedagogo) : NotFound();
        }

        // POST: api/pedagogos
        [HttpPost]
        public async Task<ActionResult<Pedagogo>> PostPedagogo(Pedagogo pedagogo)
        {
            await _context.Pedagogos.AddAsync(pedagogo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedagogo), new { codigo = pedagogo.Codigo }, pedagogo);
        }

        // PUT: api/pedagogos/{codigo}
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutPedagogo(int codigo, Pedagogo pedagogo)
        {
            if (codigo != pedagogo.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(pedagogo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedagogoExists(codigo))
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

        // DELETE: api/pedagogos/{codigo}
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeletePedagogo(int codigo)
        {
            var pedagogo = await _context.Pedagogos.FindAsync(codigo);
            if (pedagogo == null)
            {
                return NotFound();
            }

            _context.Pedagogos.Remove(pedagogo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedagogoExists(int codigo)
        {
            return _context.Pedagogos.Any(e => e.Codigo == codigo);
        }
    }
}
