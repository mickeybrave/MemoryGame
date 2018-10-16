using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemoryGame.Pages.Records
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MemoryGame.Models.MemoryGameContext _context;

        public CreateModel(MemoryGame.Models.MemoryGameContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Record Record { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Record.Add(Record);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}