using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using MemoryGame.Infra;
using Microsoft.AspNetCore.Identity;
using MemoryGame.Areas.Identity.Data;
using System;

namespace MemoryGame.Pages.Lists
{

    public class EditModel : ApplicationPageBase
    {
        private readonly UserManager<User> _userManager;
        public EditModel(MemoryGameContext context, UserManager<User> userManager) : base(context)
        {
            Header = "Edit";
            _userManager = userManager;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentUser = await _userManager.GetUserAsync(this.User);
            List.User = currentUser ?? throw new UnauthorizedAccessException($"User {this.User.Identity.Name} cannot be found in the database");
            List.UserId = currentUser.Id;

            _context.Attach(List).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(List.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ListExists(int id)
        {
            return _context.List.Any(e => e.ID == id);
        }
    }
}
