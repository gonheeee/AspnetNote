using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetNote.MVC6.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost, Route("api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // 이미지나 파일을 업로드 할 때 필요한 구성
            // 1. Path - 어디에 저장할지
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");

            // 2. Name - DateTime, GUID + GUID
            // 3. Extension (확장자) - jpeg, png ... etc.
            var fileFullName = file.FileName.Split('.');
            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";

            using(var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trumbowyg의 upload 플러그인 upload 프로세스 담당 ajax에 이미지 경로와 성공여부를 보내줘야 함.
            return Ok(new { file = "/images/upload/" + fileName, success = true});

            // # URL 접근 방식
            // ASP.NET - 호스트명/ + api/upload
            // JS - 호스트명 + api/upload       => http://www.example.comapi/upload
            // JS - 호스트명 + / + api/upload   => http://www.example.com/api/upload
        }
    }
}
