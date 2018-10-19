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


        public async Task OnGetAsync(int? id)
        {
            Record = await _context.Record.ToListAsync();
        }

    }
}
