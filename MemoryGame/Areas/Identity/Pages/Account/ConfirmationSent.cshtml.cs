using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemoryGame.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmationSentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}