using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemoryGame.Models;
using MemoryGame.Infra;
using MemoryGame.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace MemoryGame.Pages.Lists
{
    public class CreateModel : ApplicationPageBase
    {
        private readonly UserManager<User> _userManager;

        public CreateModel(MemoryGameContext context, UserManager<User> userManager) : base(context)
        {
            Header = "Create";
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public List List { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentUser = await _userManager.GetUserAsync(this.User);
            List.User = currentUser ?? throw new UnauthorizedAccessException($"User {this.User.Identity.Name} cannot be found in the database");
            List.UserId = currentUser.Id;

            _context.List.Add(List);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}