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
        public bool? IsValidGuess { get; set; }
        private int _wrongGuesses;
        private const int ConstMaxWrongGuesses = 3;
        #endregion

        #region Private Methods
        public List<RecordDecorator> GetGameRecords(IList<RecordDecorator> allList)
        {
            var arrDec = allList.ToArray();
            var excludeIdexes = arrDec.FindAllIndexOf(x => x.IsGuessed);
            var indexes = _randomService.GetRandoms(null, 0, allList
                .Count(w => !w.IsGuessed), 3, excludeIdexes);
            return indexes.Select(s => arrDec[s]).ToList();
        }
        private void RevaluateGameParameters()
        {
            GameRecordDecorators = GetGameRecords(AllRecordDecorators);
            RecordToGuess = GameRecordDecorators[_randomService.GetRandom(0, 2)];
        }
        #endregion


        #region Get
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IsValidGuess = null;

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

            RevaluateGameParameters();

            _sessionHelper.AddRenewItem(this.ToString(), this);
            if (AllRecordDecorators == null)
            {
                return NotFound("No records where found");
            }

            return Page();

        }


        public async Task<IActionResult> OnGetAsync_2(int? id)
        {
            GameModel model = (GameModel)_sessionHelper.GetItem(this.ToString());

            if (model == null)//game is starting
            {//in this case id is listId
                if (id == null)
                {
                    return NotFound();
                }
                IsValidGuess = null;

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

            }
            else
            {
                //game is running
                //in this case id is record id
                AllRecordDecorators = model.AllRecordDecorators;
                GameRecordDecorators = model.GameRecordDecorators;
                RecordToGuess = model.RecordToGuess;

                if (AllRecordDecorators.All(a => a.IsGuessed))
                {
                    _sessionHelper.RemoveItem(this.ToString());
                    //you won!
                    //Show message, score and redirect
                    return Page();

                }

                if (RecordToGuess.ID == id)
                {
                    //correct guess
                    RecordToGuess.IsGuessed = true;
                    IsValidGuess = true;
                }
                else
                {
                    //wrong guess
                    //show error
                    _wrongGuesses++;
                    if (_wrongGuesses == ConstMaxWrongGuesses)
                    {
                        //game over
                        _sessionHelper.RemoveItem(this.ToString());
                    }
                }
            }



            RevaluateGameParameters();

            _sessionHelper.AddRenewItem(this.ToString(), this);
            if (AllRecordDecorators == null)
            {
                return NotFound("No records where found");
            }

            return Page();

        }
        #endregion

        //public async Task<IActionResult> GameOver(bool isWon)
        //{
        //    if (isWon)
        //    {
        //        return RedirectToPage("./Index");
        //    }
        //    else
        //    {
        //        return RedirectToPage("./Index");
        //    }
        //}
        #region Post
        public async Task<IActionResult> OnPostApplyGuess(int itemId)
        {

            GameModel model = (GameModel)_sessionHelper.GetItem(this.ToString());
            AllRecordDecorators = model.AllRecordDecorators;
            GameRecordDecorators = model.GameRecordDecorators;
            RecordToGuess = model.RecordToGuess;
            var count = AllRecordDecorators.Count(c => c.IsGuessed);
            if (AllRecordDecorators.All(a => a.IsGuessed))
            {
                //you won!
                //Show message, score and redirect
                return Page();
            }

            if (RecordToGuess.ID == itemId)
            {
                //correct guess
                RecordToGuess.IsGuessed = true;
                IsValidGuess = true;
            }
            else
            {
                //wrong guess
                //show error
                IsValidGuess = false;
                _wrongGuesses++;
                if (_wrongGuesses == ConstMaxWrongGuesses)
                {
                    //game over
                }
            }

            RevaluateGameParameters();
            _sessionHelper.AddRenewItem(this.ToString(), this);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            return Page();
        }

        #endregion

    }
}