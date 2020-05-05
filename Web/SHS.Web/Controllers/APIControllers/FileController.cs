using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHS.Core;

namespace SHS.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnv;
        public FileController(IWebHostEnvironment env)
        {
            _hostEnv = env;
        }
        [HttpPost]
        [Route("UploadImageApi")]
        public async Task<ActionResult<ResultData>> UploadImage()
        {
            var imgFile = Request.Form.Files[0];
            if (imgFile != null && !string.IsNullOrEmpty(imgFile.FileName))
            {
                var filename = imgFile.FileName.Trim();
                var extname = Path.GetExtension(filename);
                var fileSize = imgFile.Length;
                if ((fileSize / 1024 / 1024) > 1)
                {
                    return new ResultData(ReturnCode.Error, -1, "只允许上传小于 1MB 的图片", null);
                }
                var saveFilename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000, 9999) + extname;
                var path = _hostEnv.WebRootPath;
                string dir = DateTime.Now.ToString("yyyyMMdd");
                string finaldir = path + Path.DirectorySeparatorChar + "file" + Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar + "images" + Path.DirectorySeparatorChar + dir;
                if (!Directory.Exists(finaldir))
                {
                    Directory.CreateDirectory(finaldir);
                }
                var completeFilename = finaldir + Path.DirectorySeparatorChar + saveFilename;
                using (FileStream fs = System.IO.File.Create(completeFilename))
                {
                    await imgFile.CopyToAsync(fs);
                    fs.Flush();
                }
                return new ResultData(ReturnCode.Succeed, -1, "上传成功",new { src = $"/file/upload/images/{dir}/{saveFilename}" });
            }
            return new ResultData(ReturnCode.Error, -1, "图片上传失败", null);
        }
    }
}