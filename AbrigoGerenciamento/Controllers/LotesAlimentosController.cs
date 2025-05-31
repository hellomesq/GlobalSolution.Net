using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;
using AbrigoGerenciamento.DTOs;

namespace AbrigoGerenciamento.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class LotesAlimentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public LotesAlimentosController(AppDbContext context)
    {
        _context = context;
    }

    // Listar lotes por abrigo
    [HttpGet("abrigo/{abrigoId}")]
    public async Task<ActionResult<List<LoteAlimentoReadDto>>> GetLotesPorAbrigo(int abrigoId)
    {
        var lotes = await _context.LotesAlimentos
            .Where(l => l.AbrigoId == abrigoId)
            .ToListAsync();

        // Conversão manual para DTO
        var lotesDto = lotes.Select(l => new LoteAlimentoReadDto
        {
            Id = l.Id,
            Nome = l.Nome,
            Quantidade = l.Quantidade,
            AbrigoId = l.AbrigoId
        }).ToList();

        return Ok(lotesDto);
    }

    // Consultar lote pelo id
    [HttpGet("{id}")]
    public async Task<ActionResult<LoteAlimentoReadDto>> GetLotePorId(int id)
    {
        var lote = await _context.LotesAlimentos.FindAsync(id);
        if (lote == null)
            return NotFound();

        var loteDto = new LoteAlimentoReadDto
        {
            Id = lote.Id,
            Nome = lote.Nome,
            Quantidade = lote.Quantidade,
            AbrigoId = lote.AbrigoId
        };

        return Ok(loteDto);
    }

    // Atualizar lote
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLote(int id, LoteAlimentoUpdateDto loteDto)
    {
        var lote = await _context.LotesAlimentos.FindAsync(id);
        if (lote == null)
            return NotFound();

        lote.Nome = loteDto.Nome;
        lote.Quantidade = loteDto.Quantidade;

        _context.LotesAlimentos.Update(lote);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Criar novo lote
    [HttpPost]
    public async Task<ActionResult<LoteAlimentoReadDto>> Post(LoteAlimentoCreateDto loteCreateDto)
    {
        var abrigoExiste = await _context.Abrigos.AnyAsync(a => a.Id == loteCreateDto.AbrigoId);
        if (!abrigoExiste)
            return NotFound($"Abrigo com ID {loteCreateDto.AbrigoId} não encontrado.");

        var lote = new LoteAlimento
        {
            Nome = loteCreateDto.Nome,
            Quantidade = loteCreateDto.Quantidade,
            AbrigoId = loteCreateDto.AbrigoId
        };

        _context.LotesAlimentos.Add(lote);
        await _context.SaveChangesAsync();

        var loteReadDto = new LoteAlimentoReadDto
        {
            Id = lote.Id,
            Nome = lote.Nome,
            Quantidade = lote.Quantidade,
            AbrigoId = lote.AbrigoId
        };

        return CreatedAtAction(nameof(GetLotePorId), new { id = lote.Id }, loteReadDto);
    }
}

}