namespace AbrigoGerenciamento.DTOs
{
    public class AbrigoCreateDto
    {
        public required string Nome { get; set; }
        public required string Cep { get; set; }

    }

    public class AbrigoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
    }
    
    public class LoteAlimentoCreateDto
    {
        public required string Nome { get; set; }
        public int Quantidade { get; set; }
        public required int AbrigoId { get; set; }
    }

    public class LoteAlimentoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int AbrigoId { get; set; }
    }
}