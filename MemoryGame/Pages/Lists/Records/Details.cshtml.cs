using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemoryGame.Pages.Records
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MemoryGame.Models.MemoryGameContext _context;

        public DetailsModel(MemoryGame.Models.MemoryGameContext context)
        {
            _context = context;
        }
        public Record Record { get; set; }

        public int ListId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, int? listId)
        {
            if (id == null || ListId == null)
            {
                return NotFound();
            }
            ListId = listId.Value;
            Record = await _context.Record
                .FirstOrDefaultAsync(m => m.ID == id && listId == m.ListId);

            if (Record == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
