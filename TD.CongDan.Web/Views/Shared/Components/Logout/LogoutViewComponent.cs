using Microsoft.AspNetCore.Mvc;

namespace TD.CongDan.Web.Views.Shared.Components.Logout
{
    public class LogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}