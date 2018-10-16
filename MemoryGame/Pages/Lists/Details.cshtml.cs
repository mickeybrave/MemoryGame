using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemoryGame.Pages.Lists
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MemoryGame.Models.MemoryGameContext _context;

        public DetailsModel(MemoryGame.Models.MemoryGameContext context)
        {
            _context = context;
        }

        public List List { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List = await _context.List.FirstOrDefaultAsync(m => m.ID == id);

            if (List == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
