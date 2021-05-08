using TD.CongDan.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace TD.CongDan.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            return View();
        }
    }
}