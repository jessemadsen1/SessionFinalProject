using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace SessionFinalProject.Pages
{
    public class loginModel : PageModel
    {
        private readonly UserContext userContext;
        public string Message { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public loginModel(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            var user = await userContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return RedirectToPage("/Signup", new { message = "Sign in" });
            }
            else
            {
                var hashedPassword = PasswordHasher.HashPasword(password, user.Salt);
                if (hashedPassword == user.HashedPassword)
                {
                    var session = new Sessions
                    {
                        User = user,
                        ExpireOn = DateTime.Now.AddDays(7),
                        SessionCode = Guid.NewGuid().ToString()
                    };
                    userContext.Sessions.Add(session);
                    await userContext.SaveChangesAsync();

                    Response.Cookies.Append("SessionCode", session.SessionCode, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30)
                    });

                    return RedirectToPage("/Index1");
                }
                return RedirectToPage("/Signup", new { message = "Sign in" });
            }
        }
    }
}
