namespace AbrigoGerenciamento.DTOs
{
    public class AbrigoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class AbrigoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class AbrigoComLotesReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public List<LoteAlimentoReadDto> LotesAlimentos { get; set; } = new();
    }

    public class AbrigoLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
    
    public class LoteAlimentoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public int Peso { get; set; }
        public int AbrigoId { get; set; }
    }

    public class LoteAlimentoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Peso { get; set; }
        public int AbrigoId { get; set; }
    }

    public class LoteAlimentoUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
        public int Peso { get; set; }
    }
}