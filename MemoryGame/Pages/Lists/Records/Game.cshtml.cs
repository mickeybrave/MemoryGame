using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryGame.Infra;
using MemoryGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame.Pages.Lists.Records
{
    public class GameModel : ApplicationPageBase
    {
        public GameModel(MemoryGameContext context) :
            base(context)
        {
        }

        public IList<Record> Record { get; set; }
        public int ListId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ListId = id.Value;
            this.Header = (await _context.List
                .FirstOrDefaultAsync(a => a.ID == id.Value))
                   .Caption;

            var records = from r in _context.Record
                          select r;

            records = records.Where(w => w.ListId == id);


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