using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;
using AbrigoGerenciamento.DTOs;

namespace AbrigoGerenciamento.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbrigosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AbrigosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista todos os abrigos.")]
    [SwaggerResponse(200, "Lista de abrigos retornada com sucesso.")]
    public async Task<ActionResult<IEnumerable<AbrigoReadDto>>> Get()
    {
        var abrigos = await _context.Abrigos
            .AsNoTracking()
            .Select(a => new AbrigoReadDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Cep = a.Cep,
                Email = a.Email,
                Senha = a.Senha
            })
            .ToListAsync();

        return Ok(abrigos);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém um abrigo pelo ID.")]
    [SwaggerResponse(200, "Abrigo retornado com sucesso.")]
    [SwaggerResponse(404, "Abrigo não encontrado.")]
    public async Task<ActionResult<AbrigoReadDto>> GetById(int id)
    {
        var abrigo = await _context.Abrigos.FindAsync(id);
        if (abrigo == null)
            return NotFound();

        var dto = new AbrigoReadDto
        {
            Id = abrigo.Id,
            Nome = abrigo.Nome,
            Cep = abrigo.Cep,
            Email = abrigo.Email,
            Senha = abrigo.Senha
        };

        return Ok(dto);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo abrigo.")]
    [SwaggerResponse(201, "Abrigo criado com sucesso.")]
    public async Task<ActionResult<AbrigoReadDto>> Post(AbrigoCreateDto dto)
    {
        var abrigo = new Abrigo
        {
            Nome = dto.Nome,
            Cep = dto.Cep,
            Email = dto.Email,
            Senha = dto.Senha
        };

        _context.Abrigos.Add(abrigo);
        await _context.SaveChangesAsync();

        var abrigoReadDto = new AbrigoReadDto
        {
            Id = abrigo.Id,
            Nome = abrigo.Nome,
            Cep = abrigo.Cep,
            Email = abrigo.Email,
            Senha = abrigo.Senha
        };

        return CreatedAtAction(nameof(GetById), new { id = abrigo.Id }, abrigoReadDto);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza os dados de um abrigo existente.")]
    [SwaggerResponse(204, "Abrigo atualizado com sucesso.")]
    [SwaggerResponse(404, "Abrigo não encontrado.")]
    public async Task<IActionResult> Put(int id, AbrigoCreateDto dto)
    {
        var abrigo = await _context.Abrigos.FindAsync(id);
        if (abrigo == null)
            return NotFound();

        abrigo.Nome = dto.Nome;
        abrigo.Cep = dto.Cep;
        abrigo.Email = dto.Email;
        abrigo.Senha = dto.Senha;

        _context.Abrigos.Update(abrigo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove um abrigo pelo ID.")]
    [SwaggerResponse(204, "Abrigo removido com sucesso.")]
    [SwaggerResponse(404, "Abrigo não encontrado.")]
    public async Task<IActionResult> Delete(int id)
    {
        var abrigo = await _context.Abrigos.FindAsync(id);
        if (abrigo == null)
            return NotFound();

        _context.Abrigos.Remove(abrigo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Autentica um abrigo com email e senha.")]
    [SwaggerResponse(200, "Login bem-sucedido.")]
    [SwaggerResponse(401, "Credenciais inválidas.")]
    public async Task<ActionResult<AbrigoReadDto>> Login(AbrigoLoginDto dto)
    {
        var abrigo = await _context.Abrigos
            .FirstOrDefaultAsync(a => a.Email == dto.Email && a.Senha == dto.Senha);

        if (abrigo == null)
            return Unauthorized("Email ou senha inválidos.");

        var abrigoReadDto = new AbrigoReadDto
        {
            Id = abrigo.Id,
            Nome = abrigo.Nome,
            Cep = abrigo.Cep,
            Email = abrigo.Email,
            Senha = abrigo.Senha // Se quiser ocultar, basta remover aqui.
        };

        return Ok(abrigoReadDto);
    }

}
