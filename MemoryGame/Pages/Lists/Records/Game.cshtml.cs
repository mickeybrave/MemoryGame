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
        #region Fields
        private int _wrongAtempts = 0;
        #endregion

        #region Properties
        public IList<Record> Record { get; set; }
        public int ListId { get; set; }
        public IList<RecordDecorator> AllRecordDecorators { get; set; }
        public IList<RecordDecorator> GameRecordDecorators { get; set; }
        #endregion


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
            AllRecordDecorators = Record.Select(s => new RecordDecorator(s)).ToList();
            var result = Enumerable.Range(0, AllRecordDecorators.Count)
                .OrderBy(g => Guid.NewGuid()).Take(3).ToArray();

            //GameRecordDecorators
            if (Record == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
         

          

            return RedirectToPage("./Index");
        }
    }
}