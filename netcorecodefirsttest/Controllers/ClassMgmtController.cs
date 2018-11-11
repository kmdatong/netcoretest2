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
           
            return View();
        }

        public JsonResult DataList(string name,int page=1,int limit=10)
        {
            var query = _context.ClassInfo.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x=>x.Name.Contains(name.Trim()));
            }
             var list = query.Skip((page - 1)*limit)
                            .Take(limit)
                            .ToList();

            return Json(new {code=0,data= list });
        }

        public IActionResult Add(int id=0)
        {
           if(id==0)
            return View(new ClassInfo());

           var model = _context.ClassInfo.FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        public IActionResult Save(ClassInfo model)
        {
            ClassInfo entity = null;
            if (model.Id == 0)
            {
                entity = new ClassInfo();
                entity.CreateTime = DateTime.Now;
                _context.ClassInfo.Add(entity);
            }
            else {
                entity = _context.ClassInfo.FirstOrDefault(x=>x.Id == model.Id);
            }

            entity.Name = model.Name;
            entity.Remark = model.Remark;

            _context.SaveChanges();

            return RedirectToAction("List");
        }

    }
}