using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame.Models
{
    public class List
    {
        public int ID { get; set; }

        [StringLength(25, MinimumLength = 3, ErrorMessage = "Title should be in range between 3 and 25")]
        [Display(Name = "Title")]

        public string Caption { get; set; }

        //public virtual User User { get; set; }

        //[Key, ForeignKey("User")]
        public string UserId { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
