using System.ComponentModel.DataAnnotations.Schema;

namespace LabSchool.Dto
{
    public class PedagogoDto
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public long CPF { get; set; }
        public int QtdAtendimentos { get; set; } = 0;
    }

}
    