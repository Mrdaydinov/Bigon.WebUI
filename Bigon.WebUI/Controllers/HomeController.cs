using Bigon.WebUI.Models;
using Bigon.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Bigon.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Subscribe(string email)
        {
            if (email == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Field is empty"
                });
            }
            bool isEmail = Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if(!isEmail)
            {
                return Json(new
                {
                    error = true,
                    message = "Not valid email"
                });
            }

            var dbEmail = _context.Subscribers.FirstOrDefault(x=>x.EmailAddress == email);
            if(dbEmail != null && !dbEmail.IsApproved)
            {
                return Json(new
                {
                    error = true,
                    message = "Email not approved"
                });
            }

            var newSubscriber = new Subscriber
            {
                EmailAddress = email,
                CreatedAt = DateTime.Now,
            };
            
            _context.Subscribers.Add(newSubscriber);
            _context.SaveChanges(); 
            
            return Ok(new
            {
                success = true,
                message = $"Register is success"
            });
        }
    }
}
