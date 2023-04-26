using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SessionFinalProject.Pages
{
    public class createaccountModel : PageModel
    {
        private readonly UserContext userContext;
        public string Message { get; set; }
        public string Code { get; set; }
        public createaccountModel(UserContext   userContext)
        {
            this.userContext = userContext;
        }
        public async Task<IActionResult> OnGet(string code)
        {
            var link =  userContext.SignupCodes.FirstOrDefault(l => l.Code == code);
            if (link == null)
            {
                Message = "Invalid link";
            }
            else if (link.ExpiersOn < DateTime.Now)
            {
                Message = "Link expired";
            }
            else
            {
                Code = code;
                Message = "Create your account";
            }

            return Page();
            
        }


        public async Task<IActionResult> OnPost(string password, string code)
        {
            var hashedPassword = PasswordHasher.HashPasword(password, out var salt);
            var link = userContext.SignupCodes.FirstOrDefault(l => l.Code == code);
            var email = link.Email;

            var user = new User
            {
                Email = email,
                HashedPassword = hashedPassword,
                Salt = Convert.ToHexString(salt)
            };
            userContext.Users.Add(user);
            await userContext.SaveChangesAsync();
            return RedirectToPage("/login");
        }
    }
}
