using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame.Models
{
    public class Record
    {
        public int ID { get; set; }
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Word should be in range between 3 and 25")]
        [Display(Name = "Title")]

        public string Word { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Translation should be in range between 3 and 30")]
        public string Translation { get; set; }

        public int ListId { get; set; }
    }
}
