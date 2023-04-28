using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SessionFinalProject.Pages.Shared
{
    public class LogoutModel : PageModel
    {
        private readonly UserContext userContext;
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }
        public string SessionCode { get; set; }
        public LogoutModel(UserContext userContext)
        {
            this.userContext = userContext;
        }


        public async Task<IActionResult> OnGet()
        {
            string sessionCode = Request.Cookies["SessionCode"];
            if (sessionCode == null)
            {
                return RedirectToPage("/Login", new { message = "Log in" });
            }
            else
            {
                var sessionWhole = await userContext.Sessions
                        .Include(s => s.User)
                        .FirstOrDefaultAsync(s => s.SessionCode == sessionCode.ToString());
                if (sessionWhole == null)
                {
                    return RedirectToPage("/Login", new { message = "Log in" });
                }
                else
                {
                    if (sessionWhole.ExpireOn > DateTime.Now)
                    {
                        IsLoggedIn = true;
                        var user = await userContext.Users.FirstOrDefaultAsync(u => u.Id == sessionWhole.User.Id);
                        Email = user.Email;
                        IsAdmin = await userContext.UserAuthorizations.AnyAsync(ua => ua.UserId == user.Id && ua.AuthorizationId == 1);
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Login", new { message = "Log in" });
                    }
                }
            }

        }

        public async Task<IActionResult> OnPost()
        {
            string session = Request.Cookies["SessionCode"];
            if (session == null)
            {
                return RedirectToPage("/Login", new { message = "Log in" });
            }
            else
            {
                var deleteSession = await userContext.Sessions.FirstOrDefaultAsync(s => s.SessionCode == session.ToString());
                userContext.Sessions.Remove(deleteSession);
                await userContext.SaveChangesAsync();
                Response.Cookies.Delete("SessionCode");
                return RedirectToPage("/Index", new { message = "You are logged out" });
            }
        }
    }
}