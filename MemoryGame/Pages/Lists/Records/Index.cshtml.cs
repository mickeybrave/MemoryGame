using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MemoryGame.Infra;

namespace MemoryGame.Pages.Records
{
    public class IndexModel : ApplicationPageBase
    {

        public IndexModel(MemoryGameContext context) : base(context)
        {
        }

        public IList<Record> Record { get; set; }
        public int ID { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }
            ID = id.Value;
            this.Header = (await _context.List
                  .FirstOrDefaultAsync(a => a.ID == id.Value))
                     .Caption;

            var records = from r in _context.Record
                          select r;

            records = records.Where(w => w.ListId == id);

            if (!string.IsNullOrEmpty(searchString))
            {
                records = records.Where(s => s.Word.Contains(searchString));
            }

            if (records == null)
            {
                return NotFound("No records where found");
            }

            Record = await records.ToListAsync();
            if (Record == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
