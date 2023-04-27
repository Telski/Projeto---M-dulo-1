using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LabSchool.Enums;

namespace LabSchool.Models
{
    public class Aluno : Pessoa
    {
        [Required]
        [Column("SituacaoMatricula")]
        public int Situacao { get; set; }

        [Required, Range(0, 10)]
        public float Nota { get; set; }
        [Required]
        [Column("QtdAtendimentos")]
        public int Atendimentos { get; set; } = 0;
    }
} 
