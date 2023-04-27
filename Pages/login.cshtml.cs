using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SessionFinalProject.Pages
{
    public class loginModel : PageModel
    {
        private readonly UserContext userContext;
        public string Message { get; set; }
        public string Email { get; set; }
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
                    Message = "Login successful";
                    return RedirectToPage("/Index1", new { email });
                }
                return RedirectToPage("/Signup", new { message = "Sign in" });
            }
        }
    }
}
