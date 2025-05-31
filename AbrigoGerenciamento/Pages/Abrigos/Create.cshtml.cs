using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AbrigoGerenciamento.Data;
using AbrigoGerenciamento.Models;

namespace AbrigoGerenciamento.Pages.Abrigos
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Abrigo Abrigo { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Abrigos.Add(Abrigo);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}