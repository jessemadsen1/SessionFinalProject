using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SessionFinalProject.Pages.Shared
{
    public class SessionsModel : PageModel
    {
        private readonly UserContext userContext;
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }


        public SessionsModel(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public void OnGet()
        {
        }
    }
}
