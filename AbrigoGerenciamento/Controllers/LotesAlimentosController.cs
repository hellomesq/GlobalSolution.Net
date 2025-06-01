using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;
using AbrigoGerenciamento.DTOs;

namespace AbrigoGerenciamento.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LotesAlimentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public LotesAlimentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("abrigo/{abrigoId}")]
    [SwaggerOperation(Summary = "Lista todos os lotes de alimentos de um abrigo específico.")]
    [SwaggerResponse(200, "Lista de lotes retornada com sucesso.")]
    public async Task<ActionResult<List<LoteAlimentoReadDto>>> GetLotesPorAbrigo(int abrigoId)
    {
        var lotes = await _context.LotesAlimentos
            .Where(l => l.AbrigoId == abrigoId)
            .ToListAsync();

        var lotesDto = lotes.Select(l => new LoteAlimentoReadDto
        {
            Id = l.Id,
            Nome = l.Nome,
            Quantidade = l.Quantidade,
            AbrigoId = l.AbrigoId
        }).ToList();

        return Ok(lotesDto);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Obtém um lote de alimento pelo ID.")]
    [SwaggerResponse(200, "Lote retornado com sucesso.")]
    [SwaggerResponse(404, "Lote não encontrado.")]
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

    [HttpGet("com-lotes")]
    [SwaggerOperation(Summary = "Lista os abrigos com seus respectivos lotes de alimentos.")]
    [SwaggerResponse(200, "Abrigos e lotes retornados com sucesso.")]
    public async Task<ActionResult<IEnumerable<AbrigoComLotesReadDto>>> GetComLotes()
    {
        var abrigos = await _context.Abrigos
            .Include(a => a.LotesAlimentos)
            .AsNoTracking()
            .Select(a => new AbrigoComLotesReadDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Cep = a.Cep,
                LotesAlimentos = a.LotesAlimentos.Select(l => new LoteAlimentoReadDto
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    Quantidade = l.Quantidade,
                    AbrigoId = l.AbrigoId
                }).ToList()
            })
            .ToListAsync();

        return Ok(abrigos);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo lote de alimento para um abrigo.")]
    [SwaggerResponse(201, "Lote criado com sucesso.")]
    [SwaggerResponse(404, "Abrigo não encontrado.")]
    public async Task<ActionResult<LoteAlimentoReadDto>> Post(LoteAlimentoCreateDto loteCreateDto)
    {
        var abrigo = await _context.Abrigos.FirstOrDefaultAsync(a => a.Id == loteCreateDto.AbrigoId);
        if (abrigo == null)
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

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza um lote de alimento existente.")]
    [SwaggerResponse(204, "Lote atualizado com sucesso.")]
    [SwaggerResponse(404, "Lote não encontrado.")]
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

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Exclui um lote de alimento pelo ID.")]
    [SwaggerResponse(204, "Lote excluído com sucesso.")]
    [SwaggerResponse(404, "Lote não encontrado.")]
    public async Task<IActionResult> DeleteLote(int id)
    {
        var lote = await _context.LotesAlimentos.FindAsync(id);
        if (lote == null)
            return NotFound();

        _context.LotesAlimentos.Remove(lote);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
