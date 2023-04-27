using LabSchool.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabSchool.Dto
{
    public class ProfessorDto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string FormacaoAcademica { get; set; }
        public string Experiencia { get; set; }
        public string Estado { get; set; }
    }
}
