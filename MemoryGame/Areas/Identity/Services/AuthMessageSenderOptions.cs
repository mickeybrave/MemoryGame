namespace MemoryGame.Areas.Identity.Services
{
    //Email confirmation
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
