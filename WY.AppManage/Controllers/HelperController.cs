using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WY.AppManage.Controllers
{
    //[Produces("application/json")]
    public class HelperController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromServices]IHostingEnvironment env, IFormFile file)
        {

            var fileName = Path.Combine("UploadFile", DateTime.Now.ToString("MMddHHmmss") + Path.GetExtension(file.FileName));
            using (var stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
            {

                await file.CopyToAsync(stream);
            }
            if (fileName == null)
            {
                return Ok(new { code = 0, msg = "上传失败" });
            }
            return Ok(new { code = 1, msg = "ok", fileurl = fileName });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImg([FromServices]IHostingEnvironment env, IFormFile file)
        {
            if (!(Path.GetExtension(file.FileName) == ".jpg" || Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpeg"))
            {
                return Ok(new { code = 0, msg = "上传失败,文件格式不正确" });

            }
            var fileName = Path.Combine("UploadImg", DateTime.Now.ToString("MMddHHmmss") + Path.GetExtension(file.FileName));
            using (var stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }
            if (fileName == null)
            {
                return Ok(new { code = 0, msg = "上传失败" });
            }
            return Ok(new { code = 1, msg = "ok", fileurl = fileName });
        }
    }
}