using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemoryGame.Pages.Lists
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MemoryGameContext _context;

        public IndexModel(MemoryGameContext context)
        {
            _context = context;
        }

        public IList<List> List { get;set; }

        public async Task OnGetAsync()
        {
            List = await _context.List.ToListAsync();
        }
    }
}
