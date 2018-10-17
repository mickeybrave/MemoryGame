using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;
using MemoryGame.Infra;

namespace MemoryGame.Pages.Lists
{
    public class DetailsModel : ApplicationPageBase
    {
        public DetailsModel(MemoryGameContext context):base(context)
        {
            Header = "Details";
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
