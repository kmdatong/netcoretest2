using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace netcorecodefirsttest.Controllers
{
    public class FileMgmtController : Controller
    {
        private IHostingEnvironment _host;

        public FileMgmtController(
            IHostingEnvironment host
            )
        {
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFile()
        {

            return View();
        }

        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            string mapPth = _host.WebRootPath + "/fileupload/" + Guid.NewGuid().ToString("n")+".jpg";
            // full path to file in temp location
           
            Directory.CreateDirectory(Path.GetDirectoryName(mapPth));
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(mapPth, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            
            return Ok(new { path = _host.WebRootPath });
        }

        public async Task<IActionResult> FormDataUploadFile()
        {
            
            string mapPth = _host.WebRootPath + "/fileupload/" + Guid.NewGuid().ToString("n") + ".jpg";
            // full path to file in temp location
            var formFile = Request.Form.Files[0];
            Directory.CreateDirectory(Path.GetDirectoryName(mapPth));
            
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(mapPth, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
           
            return Ok(new { path = _host.WebRootPath });
        }
    }
}