using LabSchool.Models;
using LabSchool.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {
        private readonly LabSchoolContext _context;

        public ProfessoresController(LabSchoolContext context)
        {
            _context = context;
        }

        // GET: api/professores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessores()
        {
            return Ok(await _context.Professores.ToListAsync());
        }

        // GET: api/professores/{codigo}
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Professor>> GetProfessor(int codigo)
        {
            var professor = await _context.Professores.FindAsync(codigo);

            return professor != null ? Ok(professor) : NotFound();
        }

        // POST: api/professores
        [HttpPost]
        public async Task<ActionResult<Professor>> PostProfessor(Professor professor)
        {
            await _context.Professores.AddAsync(professor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfessor), new { codigo = professor.Codigo }, professor);
        }

        // PUT: api/professores/{codigo}
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutProfessor(int codigo, Professor professor)
        {
            if (codigo != professor.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(professor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorExists(codigo))
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

        // DELETE: api/professores/{codigo}
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteProfessor(int codigo)
        {
            var professor = await _context.Professores.FindAsync(codigo);
            if (professor == null)
            {
                return NotFound();
            }

            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfessorExists(int codigo)
        {
            return _context.Professores.Any(e => e.Codigo == codigo);
        }
    }
}
