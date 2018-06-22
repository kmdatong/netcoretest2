using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;

namespace netcorecodefirsttest.Controllers
{
    public class AccountController : Controller
    {
        private IMemoryCache _cache;
        public AccountController(
            IMemoryCache cache
            )
        {
            _cache = cache;
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
    }
}