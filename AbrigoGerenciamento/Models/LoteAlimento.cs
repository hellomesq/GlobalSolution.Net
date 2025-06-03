using System.ComponentModel.DataAnnotations;

namespace AbrigoGerenciamento.Models
{
    public class LoteAlimento
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Peso { get; set; }
        public int AbrigoId { get; set; }
        public Abrigo? Abrigo { get; set; }
    }
}