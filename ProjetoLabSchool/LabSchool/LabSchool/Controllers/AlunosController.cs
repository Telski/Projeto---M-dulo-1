using System.Collections.Generic;
using System.Threading.Tasks;
using LabSchool.Models;
using LabSchool.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSchool.Dto;
using Microsoft.IdentityModel.Tokens;
using LabSchool.Enums;
using LabSchool.Extensions;

namespace LabSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly LabSchoolContext _context;

        public AlunosController(LabSchoolContext context)
        {
            _context = context;
        }
        // GET: api/alunos/{codigo}
        [HttpGet("{codigo}")]
        public async Task<ActionResult<AlunoDto>> GetAluno(int codigo)
        {
            var aluno = await _context.Alunos.FindAsync(codigo);
            if (aluno == null)
                return NotFound("Aluno não encontrado.");
            { 

            var alunoDto = new AlunoDto
            {
                Codigo = aluno.Codigo,
                Nome = aluno.Nome,
                Telefone = aluno.Telefone,
                DataNascimento = aluno.DataNascimento,
                CPF = aluno.CPF.ToString(),
                Situacao = ((EnumSituacaoMatricula)aluno.Situacao).ToDisplayName(),
                Nota = aluno.Nota,
                Atendimentos = aluno.Atendimentos
            };

            return Ok(alunoDto);
        }
    }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos([FromQuery] string situacao)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (!string.IsNullOrEmpty(situacao))
            {
                if (Enum.TryParse<EnumSituacaoMatricula>(situacao, out var situacaoEnum))
                {
                    query = query.Where(a => (int)a.Situacao == (int)situacaoEnum);
                }
                else
                {
                    return BadRequest("Situação inválida.");
                }
            }

            var alunos = await query.ToListAsync();
            var result = new List<Aluno>();

            foreach (var aluno in alunos)
            {
                aluno.Situacao = (int)(EnumSituacaoMatricula)aluno.Situacao;
                result.Add(aluno);
            }

            return Ok(result);
        }

        // POST: api/alunos
        [HttpPost]
        public async Task<ActionResult<AlunoDto>> PostAluno([FromBody] AlunoDto alunoDto)
        {
            if (string.IsNullOrEmpty(alunoDto.Nome) ||
                string.IsNullOrEmpty(alunoDto.Telefone) ||
                alunoDto.DataNascimento == default(DateTime) ||
                string.IsNullOrEmpty (alunoDto.CPF) ||
                string.IsNullOrEmpty(alunoDto.Situacao) ||
                alunoDto.Nota < 0 || alunoDto.Nota > 10)
            {
                return BadRequest("Dados inválidos.");
            }

            var cpfExists = await _context.Alunos.AnyAsync(a => a.CPF == long.Parse(alunoDto.CPF));
            if (cpfExists)
            {
                return Conflict("CPF já cadastrado para outro aluno.");
            }
            if (!Enum.TryParse<EnumSituacaoMatricula>(alunoDto.Situacao, out var situacao))
            {
                return BadRequest("Situação inválida.");
            }

            var aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Telefone = alunoDto.Telefone,
                DataNascimento = alunoDto.DataNascimento,
                CPF = long.Parse(alunoDto.CPF),
                Situacao = (int)situacao,
                Nota = alunoDto.Nota,
                Atendimentos = 0
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            alunoDto.Codigo = aluno.Codigo;
            alunoDto.Atendimentos = aluno.Atendimentos;

            return CreatedAtAction("GetAluno", new { codigo = aluno.Codigo }, alunoDto);
        }
        [HttpPost("aluno")]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { codigo = aluno.Codigo }, aluno);
        }
        // PUT: api/alunos/{codigo}/situacao
        [HttpPut("{codigo}/situacao")]
        public async Task<IActionResult> PutAlunoSituacao(int codigo, [FromBody] AlunoSituacaoDto alunoSituacaoDto)
        {
            if (string.IsNullOrEmpty(alunoSituacaoDto.Situacao))
            {
                return BadRequest("Situação é obrigatória.");
            }

            if (!Enum.TryParse<EnumSituacaoMatricula>(alunoSituacaoDto.Situacao, out var situacao))
            {
                return BadRequest("Situação inválida.");
            }

            var aluno = await _context.Alunos.FindAsync(codigo);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            aluno.Situacao = (int)situacao;
            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(codigo))
                {
                    return NotFound("Aluno não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(aluno);
        }

        // PUT: api/alunos/{codigo}
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutAluno(int codigo, Aluno aluno)
        {
            if (codigo != aluno.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(codigo))
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

        // DELETE: api/alunos/{codigo}
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteAluno(int codigo)
        {
            var aluno = await _context.Alunos.FindAsync(codigo);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int codigo)
        {
            return _context.Alunos.Any(e => e.Codigo == codigo);
        }
    }
}
