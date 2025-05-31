using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;

namespace AbrigoGerenciamento.Pages.Abrigos
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context) => _context = context;

        public List<Abrigo> Abrigos { get; set; }

        public async Task OnGetAsync()
        {
            Abrigos = await _context.Abrigos.ToListAsync();
        }
    }
}