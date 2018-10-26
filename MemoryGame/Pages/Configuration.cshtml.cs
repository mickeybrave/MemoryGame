using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Areas.Identity.Data;
using MemoryGame.Models;
using MemoryGame.Infra;
using Microsoft.AspNetCore.Identity;

namespace MemoryGame.Pages
{
    public class ConfigurationModel : ApplicationPageBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public ConfigurationModel(MemoryGameContext context, UserManager<User> userManager,
            SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Config Config { get; set; }
        //private string _userId;
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            //_userId = userId;
            Config = await _context.Config
                .Include(c => c.User).FirstOrDefaultAsync(m => m.UserId == userId);

            if (Config == null)
            {
                return NotFound("Configuration is not found for user with id=" + userId);
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string userId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //security
            if (!User.Identity.IsAuthenticated)
            {
                return Forbid("Impossible to save config for not autenticated user");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user.Id != userId)
            {
                return Forbid("Invalid user id");
            }

            _context.Attach(Config).State = EntityState.Modified;
            Config.UserId = userId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(Config.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Lists/Index");
        }

        private bool ConfigExists(int id)
        {
            return _context.Config.Any(e => e.ID == id);
        }
    }
}
