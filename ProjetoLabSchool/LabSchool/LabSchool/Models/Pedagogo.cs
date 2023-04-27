using System.ComponentModel.DataAnnotations.Schema;

namespace LabSchool.Models
{
    public class Pedagogo : Pessoa
    {
        [Column("QtdAtendimentos")]
        public int Atendimento { get; set; }
    }
}
    