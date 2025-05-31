using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;

namespace AbrigoGerenciamento.Pages.Abrigos
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Abrigo Abrigo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Abrigo = await _context.Abrigos.FirstOrDefaultAsync(m => m.Id == id);

            if (Abrigo == null)
                return NotFound();

            return Page();
        }
    }
}