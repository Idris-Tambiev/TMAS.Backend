using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TMAS.BLL.Services;
using TMAS.DB.Models;

namespace TMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileService _fileService;
        public FilesController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult> Upload(int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    Guid guidName = Guid.NewGuid();
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var splitedName = fileName.Split('.');
                    var finalName = guidName.ToString() + '.' + splitedName[splitedName.Length - 1];
                    var fullPath = Path.Combine(pathToSave, finalName);
                    var dbPath = Path.Combine(folderName, finalName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var saveResult =await _fileService.Create(id,finalName,file.ContentType,fileName);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult> GetCards(int id)
        {
            return Ok(await _fileService.GetFiles(id));
        }

    }
}
