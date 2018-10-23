using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryGame.Infra;
using MemoryGame.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace MemoryGame.Pages.Lists.Records
{
    public class GameModel : ApplicationPageBase
    {
        private readonly IRandomService _randomService;
        private readonly ISessionHelper _sessionHelper;

        public GameModel(MemoryGameContext context, IRandomService randomService, ISessionHelper sessionHelper) :
            base(context)
        {
            _randomService = randomService;
            _sessionHelper = sessionHelper;
        }
        #region Fields
        private int _wrongAtempts = 0;

        #endregion

        #region Properties

        public int ListId { get; set; }
        public IList<RecordDecorator> AllRecordDecorators { get; set; }
        public IList<RecordDecorator> GameRecordDecorators { get; set; }
        public RecordDecorator RecordToGuess { get; set; }

        #endregion

        #region Get
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            var arrDec = AllRecordDecorators.ToArray();

            var excludeIdexes = arrDec.FindAllIndexOf(x => x.IsGuessed);



            var indexes = _randomService.GetRandoms(null, 0, AllRecordDecorators
                .Count(w => !w.IsGuessed), 3, excludeIdexes);


            GameRecordDecorators = indexes.Select(s => arrDec[s]).ToList();
            RecordToGuess = GameRecordDecorators[_randomService.GetRandom(0, 2)];
            _sessionHelper.AddRenewItem(this.ToString(), this);
            if (AllRecordDecorators == null)
            {
                return NotFound("No records where found");
            }

            return Page();

        }
      
        #endregion


        #region Post
        public async Task<IActionResult> OnPostApplyGuess(int itemId)
        {
            GameModel model = (GameModel) _sessionHelper.GetItem(this.ToString());
            AllRecordDecorators = model.AllRecordDecorators;
            GameRecordDecorators = model.GameRecordDecorators;
            RecordToGuess = model.RecordToGuess;

            if (RecordToGuess.ID == itemId)
            {
                //correct guess
            }
            else
            {
                //wrong guess
                //show error
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            return Page();
        }

        #endregion

    }
}