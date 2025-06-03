namespace AbrigoGerenciamento.DTOs
{
    
    public class AbrigoCreateDto
    {
        public required string Nome { get; set; }
        public required string Cep { get; set; }
        
        public required string Email { get; set; }
        public required string Senha { get; set; }
        
    }

    public class AbrigoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        
        public string Email { get; set; }
        
        public string Senha { get; set; }
    }
    
    public class AbrigoComLotesReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public List<LoteAlimentoReadDto> LotesAlimentos { get; set; } = new();
    }
    
    public class LoteAlimentoCreateDto
    {
        public required string Nome { get; set; }
        public int Peso { get; set; }
        public required int AbrigoId { get; set; }
    }

    public class LoteAlimentoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Peso { get; set; }
        public int AbrigoId { get; set; }
    }
    
    public class LoteAlimentoUpdateDto
    {
        public required string Nome { get; set; }
        public int Peso { get; set; }
    }
    
  

// DTO para login
    public class AbrigoLoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

}