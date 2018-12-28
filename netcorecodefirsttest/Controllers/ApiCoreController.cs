using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcorecodefirsttest.Domains;
using Microsoft.AspNetCore.Cors;
using System.Collections.Specialized;

namespace netcorecodefirsttest.Controllers
{
    [EnableCors("any")]
    [Produces("application/json")]
    [Route("api/ApiCore")]
    public class ApiCoreController : Controller
    {
        private DTContext _context { get; set; }

        public ApiCoreController(
            DTContext context)
        {

            _context = context;

        }

        [HttpGet]
        [Route("list")]
        public object List()
        {
            var list = _context.ClassInfo.ToList();

            return list;
        }

        [HttpGet]
        [Route("userlist")]
        public object UserList(int page=1,int pageSize=15,string callback="")
        {
            var list = _context.Account.OrderBy(x=>x.Id).Skip((page - 1) * pageSize).Take(pageSize);

            return new { data=list};
        }

        [HttpPost]
        [Route("adduser")]
        public JsonResult AddUser(Account user)
        {
           
            _context.Account.Add(user);
            _context.SaveChanges();

            var list = _context.Account.OrderBy(x => x.Id).Skip(0).Take(15);

            return Json(new { data = list });
        }

        [HttpGet]
        [Route("lunbolist")]
        public object LunBoList(int page = 1, int pageSize = 15, string callback = "")
        {
            var list = _context.LunBo.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

            return new { data = list };
        }

        [HttpGet]
        [Route("testForm")]
        public object testForm([FromBody] NameValueCollection formData)
        {
            string namestr = formData["username"];

            return new { };
        }

    }
}