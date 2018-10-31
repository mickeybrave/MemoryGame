using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using MemoryGame.Infra;

namespace MemoryGame.Pages.Records
{
    public class DetailsModel : ApplicationPageBase
    {
        public DetailsModel(MemoryGameContext context):base(context)
        {
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
