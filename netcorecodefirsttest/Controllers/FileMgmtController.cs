using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using netcorecodefirsttest.Domains;
using netcorecodefirsttest.Filter;

namespace netcorecodefirsttest.Controllers
{
    [AuthFilter]
    public class FileMgmtController : Controller
    {
        private IHostingEnvironment _host;
        private DTContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileMgmtController(
            DTContext context,
            IHostingEnvironment host,
            IHostingEnvironment hostingEnvironment
            )
        {
            _host = host;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFile()
        {

            return View();
        }

        public JsonResult DataList(string name, int page = 1, int limit = 10)
        {
            var query = _context.FileMgmt.AsQueryable(); 

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name.Trim()));
            }
            var list = query.Skip((page - 1) * limit)
                           .Take(limit)
                           .ToList();

            return Json(new { code = 0, data = list,page=page,limit = limit, count = query.Count()  });
        }

        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            string virtualPath = "";

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string guid = Guid.NewGuid().ToString("n");
                    string exten = Path.GetExtension(formFile.FileName);
                     virtualPath = string.Format("/fileupload/{0}/", guid);
                    string mapPth = _host.WebRootPath + virtualPath;
                    string filePth = _host.WebRootPath + virtualPath + formFile.FileName;
                    Directory.CreateDirectory(Path.GetDirectoryName(mapPth));

                    using (var stream = new FileStream(filePth, FileMode.Create))
                    {
                       await formFile.CopyToAsync(stream);
                       
                    }

                    //添加数据
                    FileMgmt entity = new FileMgmt();
                    entity.Name = formFile.Name;
                    entity.Length = (int)formFile.Length;
                    entity.Url = virtualPath;
                    entity.Guid = guid;
                    entity.CreateTime = DateTime.Now;
                    try
                    {
                        _context.FileMgmt.Add(entity);
                      await  _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            
            return Json(new { path = virtualPath });
        }

        public async Task<IActionResult> FormDataUploadFile()
        {
            var formFile = Request.Form.Files[0];
            string guid = Guid.NewGuid().ToString("n");
            string exten = Path.GetExtension(formFile.FileName);
            string virtualPath = string.Format("/fileupload/{0}/", guid);
            string mapPth = _host.WebRootPath + virtualPath;
            string filePth = _host.WebRootPath + virtualPath + formFile.FileName;
            // full path to file in temp location

            Directory.CreateDirectory(Path.GetDirectoryName(mapPth));
            
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePth, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);

                    //添加数据
                    FileMgmt entity = new FileMgmt();
                    entity.Name = formFile.FileName;
                    entity.Length = (int)formFile.Length;
                    entity.Url = virtualPath + formFile.FileName;
                    entity.Guid = guid;
                    entity.CreateTime = DateTime.Now;
                    try
                    {
                        _context.FileMgmt.Add(entity);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                }
           
            return Ok(new { path = filePth });
        }

        [HttpPost]
        public JsonResult DeleteFile(int id)
        {
            var entity = _context.FileMgmt.FirstOrDefault(x=>x.Id == id);
            if (entity == null)
                return Json(new { code=1,msg="数据不存在"});
           
                string webRootPath = _hostingEnvironment.WebRootPath;
                string filePath = webRootPath + entity.Url;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.FileMgmt.Remove(entity);
                _context.SaveChanges();
            
            return Json(new { code=0});
        }
    }
}