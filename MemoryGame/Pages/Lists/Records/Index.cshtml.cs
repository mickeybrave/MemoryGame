using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MemoryGame.Pages.Records
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MemoryGame.Models.MemoryGameContext _context;

        public IndexModel(MemoryGame.Models.MemoryGameContext context)
        {
            _context = context;
        }

        public IList<Record> Record { get; set; }

        //public async Task OnGetAsync()
        //{
        //    Record = await _context.Record.ToListAsync();
        //}
        //[BindProperty(SupportsGet = true)]
        //public string Id { get; set; }

        public async Task OnGetAsync(int id)
        {
            Record = await _context.Record.ToListAsync();
        }


        //[Microsoft.AspNetCore.Mvc.Route("Lists/{listId}/Records")]
        //public async Task GetRecordsByList(int? listId)
        //{

        //    var allRecords = await _context.Record.ToListAsync();

        //    Record = allRecords.Where(w => w.ListId == listId).ToList() ;

        //}
    }
}
