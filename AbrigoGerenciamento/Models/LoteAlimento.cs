using System.ComponentModel.DataAnnotations;

namespace AbrigoGerenciamento.Models
{
    public class LoteAlimento
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int Peso { get; set; }
        public int AbrigoId { get; set; }
        public Abrigo? Abrigo { get; set; }


    }

}