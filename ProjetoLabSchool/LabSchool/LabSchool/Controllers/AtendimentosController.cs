using LabSchool.Models;
using LabSchool.Context;
using LabSchool.Dto;
using LabSchool.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LabSchool.Dtos;

namespace LabSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentosController : ControllerBase
    {
        private readonly LabSchoolContext _context;

        public AtendimentosController(LabSchoolContext context)
        {
            _context = context;
        }

        [HttpPut("atendimentos")]
        public async Task<ActionResult> PutAtendimentoPedagogico([FromBody] AtendimentoDto atendimentoDto)
        {
            if (atendimentoDto.CodigoAluno == 0 || atendimentoDto.CodigoPedagogo == 0)
            {
                return BadRequest("Código do aluno e do pedagogo são obrigatórios.");
            }

            var aluno = await _context.Alunos.FindAsync(atendimentoDto.CodigoAluno);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            var pedagogo = await _context.Pedagogos.FindAsync(atendimentoDto.CodigoPedagogo);
            if (pedagogo == null)
            {
                return NotFound("Pedagogo não encontrado.");
            }

            aluno.Atendimentos++;
            aluno.Situacao = (int)EnumSituacaoMatricula.ATENDIMENTO_PEDAGOGICO;
            pedagogo.Atendimento++;

            _context.Entry(aluno).State = EntityState.Modified;
            _context.Entry(pedagogo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(new
            {
                Aluno = new
                {
                    aluno.Codigo,
                    aluno.Nome,
                    aluno.Telefone,
                    aluno.DataNascimento,
                    aluno.CPF,
                    Situacao = Enum.GetName(typeof(EnumSituacaoMatricula), aluno.Situacao),
                    aluno.Nota,
                    aluno.Atendimentos
                },
                Pedagogo = new
                {
                    pedagogo.Codigo,
                    pedagogo.Nome,
                    pedagogo.Telefone,
                    pedagogo.DataNascimento,
                    pedagogo.CPF,
                    pedagogo.Atendimento
                }
            });
        }

    }
}
