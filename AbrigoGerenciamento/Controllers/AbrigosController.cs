using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<IEnumerable<AbrigoReadDto>>> Get()
    {
        var abrigos = await _context.Abrigos
            .AsNoTracking()
            .Select(a => new AbrigoReadDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Cep = a.Cep
            })
            .ToListAsync();

        return Ok(abrigos);
    }

    [HttpPost]
    public async Task<ActionResult<AbrigoReadDto>> PostComLotes(AbrigoCreateComLotesDto dto)
    {
        var abrigo = new Abrigo
        {
            Nome = dto.Nome,
            Cep = dto.Cep,
            LotesAlimentos = dto.LotesAlimentos.Select(l => new LoteAlimento
            {
                Nome = l.Nome,
                Quantidade = l.Quantidade
            }).ToList()
        };

        _context.Abrigos.Add(abrigo);
        await _context.SaveChangesAsync();

        var abrigoReadDto = new AbrigoReadDto
        {
            Id = abrigo.Id,
            Nome = abrigo.Nome,
            Cep = abrigo.Cep
        };

        return CreatedAtAction(nameof(Get), new { id = abrigo.Id }, abrigoReadDto);
    }
    
    
}