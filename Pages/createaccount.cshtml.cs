using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SessionFinalProject.Pages
{
    public class createaccountModel : PageModel
    {
        private readonly UserContext userContext;
        public string Message { get; set; }
        public createaccountModel(UserContext   userContext)
        {
            this.userContext = userContext;
        }
        public async Task OnGet(string code)
        {
            var link = userContext.SignupCodes.FirstOrDefault(l => l.Code == code);
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
                Message = "Create your account";
            }
            
        }


        //public async Task<IActionResult> OnPost(string password)
        //{
        //    var newAcount = new { Email = link.Email, Password = password };
        //}
    }
}
