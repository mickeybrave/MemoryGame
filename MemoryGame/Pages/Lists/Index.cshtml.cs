using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using MemoryGame.Infra;
using Microsoft.AspNetCore.Identity;
using MemoryGame.Areas.Identity.Data;
using System.Linq;
using System;

namespace MemoryGame.Pages.Lists
{

    public class IndexModel : ApplicationPageBase
    {
        private readonly UserManager<User> _userManager;
        public IndexModel(MemoryGameContext context, UserManager<User> userManager) : base(context)
        {
            Header = "All Lists";
            _userManager = userManager;
        }

        public IList<List> List { get; set; }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(this.User);
            if (currentUser == null) throw new UnauthorizedAccessException($"User {this.User.Identity.Name} cannot be found in the database");

            var allLists = await _context.List.ToListAsync();
            List = allLists != null ? allLists.Where(w => w.UserId == currentUser.Id).ToList() : allLists;
        }
    }
}
