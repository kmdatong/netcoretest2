using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using netcorecodefirsttest.Domains;
using Microsoft.AspNetCore.Cors;

namespace netcorecodefirsttest.Controllers
{
    [EnableCors("any")]
    public class AccountController : Controller
    {
        private IMemoryCache _cache;
        private DTContext _context { get; set; }
        public AccountController(
            IMemoryCache cache,
            DTContext context
            )
        {
            _cache = cache;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(string name)
        {
            var claims = new[] { new Claim("LonginName", name) };

            ClaimsIdentity clainsident = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(clainsident);

            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               principal).Wait();

            if (_cache.TryGetValue("UserName",out var m))
            {
                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
                _cache.Set("UserName",name, options);
            }
            

            return RedirectToAction("List", "ClassMgmt");
        }


        public ActionResult LoginOut()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("List", "ClassMgmt");
        }

        [HttpGet]
        public JsonResult userlist(int page = 1, int pageSize = 15, string callback = "")
        {
            var list = _context.Account.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);


            return Json(new {data = list });
        }

        [HttpPost]
        public JsonResult AddUser(Account user)
        {
            _context.Account.Add(user);
            _context.SaveChanges();

            var list = _context.Account.OrderBy(x => x.Id).Skip(0).Take(15);

            return Json( new { data = list });
        }
    }
}