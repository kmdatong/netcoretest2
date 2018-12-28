using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcorecodefirsttest.Domains;
using netcorecodefirsttest.Models;

namespace netcorecodefirsttest.Controllers
{
    public class IsgrcController : Controller
    {

        private DTContext _context { get; set; }

        public IsgrcController(
            DTContext context)
        {

            _context = context;

        }

        public IActionResult Index(int id)
        {

            var list = _context.FormTemplate.Where(x => x.InstId == id).ToList();
            TemplateTree tree = new TemplateTree();

            tree.Id = id;
            tree.name = "制度1";
            
            foreach (var cid in list.GroupBy(x=>x.CtrlId).Select(x=>x.Key).ToList())
            {
                Ctrl ctrl = new Ctrl();
                ctrl.Id = cid;
                tree.CtrlList.Add(ctrl);
                //检查
                foreach (var checkid in list.Where(x=>x.CtrlId == cid).GroupBy(x=>x.CheckId).Select(x=>x.Key).ToList())
                {
                    
                    Check checkenitty = new Check();
                    checkenitty.Id = checkid;
                    ctrl.CheckList.Add(checkenitty);
                    //指标
                    foreach (var item in list.Where(x=>x.CheckId == checkid).GroupBy(x=>x.NormId).Select(x=>x.Key).ToList())
                    {
                        Norm nomr = new Norm { Id = item, Name = "指标"+item.ToString() };
                        checkenitty.NormList.Add(nomr);
                    }

                }
            }

            return View(tree);
        }

        public JsonResult SaveFormValue(int instId,List<NormValue> list)
        {
            FormInstance instance = new FormInstance();
            instance.InstId = instId;

            _context.FormInstance.Add(instance);
            _context.SaveChanges();

            foreach (var item in list)
            {
                if (string.IsNullOrWhiteSpace(item.Value))
                    continue;

                FormValue fvalue = new FormValue();
                fvalue.InstId = instance.Id;
                fvalue.NormId = item.Id;
                fvalue.ValueString = item.Value;

                _context.FormValue.Add(fvalue);
                _context.SaveChanges();
            }


            return Json(new { code = 0});
        }

        public ActionResult Instance(int id)
        {
            var list = _context.FormTemplate.Where(x => x.InstId == id).ToList();
            TemplateTree tree = new TemplateTree();

            int isid = _context.FormInstance.FirstOrDefault(x => x.InstId == id).Id;
            var normList = _context.FormValue.Where(x => x.InstId == isid).ToList();

            tree.Id = id;
            tree.name = "制度1";
            foreach (var cid in list.GroupBy(x => x.CtrlId).Select(x => x.Key).ToList())
            {
                Ctrl ctrl = new Ctrl();
                ctrl.Id = cid;
                tree.CtrlList.Add(ctrl);
                //检查
                foreach (var checkid in list.Where(x => x.CtrlId == cid).GroupBy(x => x.CheckId).Select(x => x.Key).ToList())
                {

                    Check checkenitty = new Check();
                    checkenitty.Id = checkid;
                    ctrl.CheckList.Add(checkenitty);
                    //指标
                    foreach (var item in list.Where(x => x.CheckId == checkid).Select(x => x.NormId).ToList())
                    {
                        var formValue = normList.FirstOrDefault(x => x.NormId == item);
                        string name = formValue == null ? "" : formValue.ValueString;
                        Norm nomr = new Norm { Id = item, Name = name };
                        
                        checkenitty.NormList.Add(nomr);
                    }

                }
            }

            return View(tree);
        }
    }
}