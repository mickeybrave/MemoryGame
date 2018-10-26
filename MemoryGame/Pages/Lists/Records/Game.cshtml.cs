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

        public GameModel(MemoryGameContext context) :
            base(context)
        {
        }
        #region Fields

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


            if (AllRecordDecorators == null)
            {
                return NotFound("No records where found");
            }

            return Page();

        }


        #endregion


    }
}