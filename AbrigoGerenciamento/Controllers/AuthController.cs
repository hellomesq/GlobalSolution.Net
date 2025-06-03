[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AbrigoRegisterDto dto)
    {
        // Verifica se já existe um abrigo com o mesmo e-mail
        var exists = await _context.Abrigos.AnyAsync(a => a.Email == dto.Email);
        if (exists)
            return BadRequest("Já existe um abrigo com esse email.");

        var abrigo = new Abrigo
        {
            Nome = dto.Nome,
            Cep = dto.Cep,
            Email = dto.Email,
            Senha = dto.Senha // Obs: salvar com hash em produção
        };

        _context.Abrigos.Add(abrigo);
        await _context.SaveChangesAsync();

        return Ok(new { abrigo.Id, abrigo.Nome, abrigo.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AbrigoLoginDto dto)
    {
        var abrigo = await _context.Abrigos
            .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Senha == dto.Senha);

        if (abrigo == null)
            return Unauthorized("Email ou senha inválidos.");

        // Se quiser retornar um token ou id:
        return Ok(new { abrigo.Id, abrigo.Nome, abrigo.Email });
    }
}