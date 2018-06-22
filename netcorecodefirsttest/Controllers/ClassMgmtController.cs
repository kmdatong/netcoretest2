using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcorecodefirsttest.Domains;
using netcorecodefirsttest.Filter;

namespace netcorecodefirsttest.Controllers
{
   [SampleAction]
    public class ClassMgmtController : Controller
    {
        private  DTContext _context { get; set; }

        public ClassMgmtController(
            DTContext context)
        {

            _context = context;
            
        }

        public IActionResult List()
        {
           
            var list = _context.ClassInfo.ToList();

            
            return View(list);
        }

        public IActionResult Add()
        {
           
            return View(new ClassInfo());
        }

        public IActionResult Save(ClassInfo model)
        {
            
            _context.ClassInfo.Add(model);
            _context.SaveChanges();

            return RedirectToAction("List");
        }

    }
}