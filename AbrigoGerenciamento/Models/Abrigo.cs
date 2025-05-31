using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbrigoGerenciamento.Models
{
    public class Abrigo
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cep { get; set; }

        public List<LoteAlimento> LotesAlimentos { get; set; } = new();

    }

}