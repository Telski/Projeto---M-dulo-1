using LabSchool.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabSchool.Models
{
    public class Professor : Pessoa
    {
        public string? Estado { get; set; }
        public string? Experiencia { get; set; }
        [Column("FormacaoAcademica")]
        public string? FormacaoAcademica { get; set; }
    }
}
