using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemoryGame.Infra
{
    //security
    [Authorize]
    public abstract class ApplicationPageBase : PageModel
    {
        public string Header { get; set; }
        protected readonly MemoryGameContext _context;

        public ApplicationPageBase(MemoryGameContext context)
        {
            _context = context;
        }
       
    }
}
