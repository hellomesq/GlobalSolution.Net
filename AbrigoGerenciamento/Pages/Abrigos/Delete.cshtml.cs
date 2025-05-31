using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;

namespace AbrigoGerenciamento.Pages.Abrigos
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Abrigo Abrigo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Abrigo = await _context.Abrigos.FindAsync(id);

            if (Abrigo == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Abrigo == null)
                return NotFound();

            var abrigo = await _context.Abrigos.FindAsync(Abrigo.Id);

            if (abrigo != null)
            {
                _context.Abrigos.Remove(abrigo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}