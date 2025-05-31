namespace AbrigoGerenciamento.DTOs
{
    public class AbrigoCreateComLotesDto
    {
        public required string Nome { get; set; }
        public required string Cep { get; set; }

        public List<LoteAlimentoSimplesCreateDto> LotesAlimentos { get; set; } = new();
    }

    public class LoteAlimentoSimplesCreateDto
    {
        public required string Nome { get; set; }
        public int Quantidade { get; set; }
    }
    
    public class LoteAlimentoUpdateDto
    {
        public required string Nome { get; set; }
        public int Quantidade { get; set; }
    }

}