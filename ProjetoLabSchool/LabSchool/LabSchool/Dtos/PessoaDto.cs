using System.ComponentModel.DataAnnotations;

namespace LabSchool.Dto
{
    public abstract class PessoaDto
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string DataNascimento { get; set; }

        public string CPF { get; set; }

    }
}