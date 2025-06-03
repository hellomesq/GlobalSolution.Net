using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrigoGerenciamento.Models
{
    public class Abrigo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public List<LoteAlimento> LotesAlimentos { get; set; } = new();
    }
}