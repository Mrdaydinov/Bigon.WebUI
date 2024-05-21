using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
