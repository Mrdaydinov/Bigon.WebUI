using Bigon.WebUI.Helpers.Services;
using Bigon.WebUI.Models;
using Bigon.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace Bigon.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public HomeController(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
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

            if (dbEmail != null && !dbEmail.IsApproved)
            {
                return Json(new
                {
                    error = true,
                    message = "Email already exists"
                });
            }

            var newSubscriber = new Subscriber
            {
                EmailAddress = email,
                CreatedAt = DateTime.Now,
            };
            
            _context.Subscribers.Add(newSubscriber);
            _context.SaveChanges();

            string token = $"#demo-{newSubscriber.EmailAddress}-{newSubscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon";
            token = HttpUtility.UrlEncode(token);

            string url = $"{Request.Scheme}://{Request.Host}/subscribe-approve?token={token}";
            string body = $"Please click to link accept subscription <a href=\"{url}\">Click!</a>";

            await _emailService.SendMailAsync(email, "Subscription", body);

            return Ok(new
            {
                success = true,
                message = $"Register is success, please verify your email"
            });
        }

        [Route("/subscribe-approve")]
        public async Task<IActionResult> SubscribeApprove(string token)
        {
            string pattern = @"#demo-(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";

            Match match = Regex.Match(token, pattern);

            if (!match.Success)
            {
                return Content("token is broken!");
            }

            string email = match.Groups["email"].Value;
            string dateStr = match.Groups["date"].Value;

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out DateTime date))
            {
                return Content("token is broken!");
            }

            var subscriber = await _context.Subscribers
                .FirstOrDefaultAsync(m => m.EmailAddress.Equals(email) && m.CreatedAt == date);

            if (subscriber == null)
            {
                return Content("token is broken!");
            }

            if (!subscriber.IsApproved)
            {
                subscriber.IsApproved = true;
                subscriber.ApprovedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();


            return Content($"Success: Email: {email}\n" +
                $"Date: {date}");
        }
    }
}
