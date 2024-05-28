using Bigon.WebUI.Models;
using Bigon.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bigon.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly DataContext _context;

        public ColorController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var colors = _context.Colors.ToList();
            return View(colors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Color color)
        {
            color.CreatedAt = DateTime.Now;
            color.CreatedBy = 1;
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
