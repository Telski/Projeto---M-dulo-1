using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LabSchool.Dto
{
    public class AlunoDto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Situacao { get; set; }
        public float Nota { get; set; }
        public int Atendimentos { get; set; }
    }
    public class CadastroAlunoDto
    {

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Situacao { get; set; }
        [Required]
        public double Nota { get; set; }
    }
    public class SituacaoMatriculaDto
    {
        public string Situacao { get; set; }
    }
    public class AlunoRespostaDto
    {
        [Required]
        public int Codigo { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Telefone { get; set; }
        [Required]
        public string? DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string? Situacao { get; set; }
        [Required]
        public float Nota { get; set; }
        [Required]
        public int QtdAtendimentos { get; set; }
    }
}


