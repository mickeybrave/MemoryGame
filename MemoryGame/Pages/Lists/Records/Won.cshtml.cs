using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;
using MemoryGame.Infra;
using Microsoft.AspNetCore.Identity;
using MemoryGame.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemoryGame.Pages.Records
{
    [Authorize]
    public class Won : ApplicationPageBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public Won(MemoryGameContext context, UserManager<User> userManager,
            SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public Config Config { get; set; }
        public string UserName { get; set; }
        public int RecordScore { get; set; }
        public int ListId { get; set; }
        public bool IsRecordIncreased { get; set; }
        public async Task<IActionResult> OnGetAsync(int? configId, int recordScore, string userId, int listId)
        {
            ListId = listId;
            if (configId == null)
            {
                return NotFound();
            }
            //security
            var user = await _userManager.GetUserAsync(User);
            if (user.Id != userId)
            {
                return NotFound("invalid user!");
            }

            UserName = user.FirstName;
            this.Config = await _context.Config.FirstOrDefaultAsync(f => f.ID == configId);

            if (this.Config.BestScore < recordScore)
            {
                IsRecordIncreased = true;
                RecordScore = recordScore;
                this.Config.BestScore = recordScore;

                _context.Attach(this.Config).State = EntityState.Modified;
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
            }
            return Page();
        }
        private bool ConfigExists(int id)
        {
            return _context.Config.Any(e => e.ID == id);
        }

    }
}
