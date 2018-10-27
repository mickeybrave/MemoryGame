using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryGame.Areas.Identity.Data;
using MemoryGame.Infra;
using MemoryGame.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MemoryGame.Pages.Lists.Records
{
    public class GameModel : ApplicationPageBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public GameModel(MemoryGameContext context, UserManager<User> userManager,
            SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Fields

        #endregion

        #region Properties

        public int ListId { get; set; }
        public IList<RecordDecorator> AllRecordDecorators { get; set; }

        public Config Config { get; set; }
        private const int ConstMaxWrongGuesses = 3;
        #endregion
        
        #region Get
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            this.Config = await _context.Config.FirstOrDefaultAsync(f => f.UserId == user.Id);

            ListId = id.Value;
            this.Header = (await _context.List
                .FirstOrDefaultAsync(a => a.ID == id.Value))
                   .Caption;

            var records = from r in _context.Record
                          select r;

            records = records.Where(w => w.ListId == id);


            if (records == null)
            {
                return NotFound("No records where found");
            }

            var recordsTemp = await records.ToListAsync();

            AllRecordDecorators = recordsTemp
                .Select(s => new RecordDecorator(s))
                    .ToList();

            if (AllRecordDecorators == null)
            {
                return NotFound("No records where found");
            }

            return Page();
        }


        #endregion

        #region Post
        public async Task<IActionResult> OnPostAsync(int configId, int recordScore, string userId, int listId)
        {
            return Redirect($"/Lists/{listId}/Records/Won?configId=" +
                $"{configId}&recordScore={recordScore}&userId={userId}&listId={listId}");
        }
               
        #endregion

    }
}