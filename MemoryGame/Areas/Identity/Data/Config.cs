using System.ComponentModel.DataAnnotations;

namespace MemoryGame.Areas.Identity.Data
{
    public class Config
    {
        public int ID { get; set; }
        [Display(Name = "Is From Foreign Language")]
        public bool IsFromForeignLanguage { get; set; }
        [Display(Name = "Best Score")]
        public int BestScore { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
