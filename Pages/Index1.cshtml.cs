using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SessionFinalProject.Pages
{
    public class Index1Model : PageModel
    {
        private readonly UserContext userContext;
        public bool isAdmin { get; set; }
        public Index1Model(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public async Task<IActionResult> OnGet(string email)
        {
            var user = await userContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return RedirectToPage("/Signup", new { message = "Sign in" });
            }
            else
            {
                isAdmin = await userContext.UserAuthorizations.AnyAsync(ua => ua.UserId == user.Id && ua.AuthorizationId == 1);

                return Page();
            }
        }
    }
}
