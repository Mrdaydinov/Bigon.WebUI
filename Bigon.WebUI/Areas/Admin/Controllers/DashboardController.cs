using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
