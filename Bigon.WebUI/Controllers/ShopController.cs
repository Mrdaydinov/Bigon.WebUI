using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Controllers
{
    public class ShopController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
