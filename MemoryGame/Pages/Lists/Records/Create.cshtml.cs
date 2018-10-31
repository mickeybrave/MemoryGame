using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Infra;

namespace MemoryGame.Pages.Records
{
    public class CreateModel : ApplicationPageBase
    {
        public CreateModel(MemoryGameContext context) : base(context)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Record Record { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Record.ListId = id.Value;
            Record.List = await _context.List
                .FirstOrDefaultAsync(f => f.ID == id.Value);
            _context.Record.Add(Record);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}