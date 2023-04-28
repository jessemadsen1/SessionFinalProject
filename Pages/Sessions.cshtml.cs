using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SessionFinalProject.Pages
{
    public class SessionsModel : PageModel
    {
        private readonly UserContext userContext;
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }
        public IEnumerable<Sessions> ActiveSessions { get; set; }

        public SessionsModel(UserContext userContext)
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

                        IsAdmin = await userContext.UserAuthorizations.AnyAsync(ua => ua.UserId == user.Id && ua.AuthorizationId == 1);
                        ActiveSessions = await userContext.Sessions
                                 .Where(s => s.ExpireOn > DateTime.Now)
                                 .Include(s => s.User)
                                 .ToListAsync();

                        foreach (var session in ActiveSessions)
                        {
                            session.SessionCode = session.SessionCode.Substring(session.SessionCode.Length - 4);
                        }

                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Login", new { message = "Log in" });
                    }
                }
            }
        }
        public async Task<IActionResult> OnPost(int sessionId)
        {
            var session = await userContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            userContext.Sessions.Remove(session);
            await userContext.SaveChangesAsync();
            return RedirectToPage("/Sessions");
        }
    }
}
