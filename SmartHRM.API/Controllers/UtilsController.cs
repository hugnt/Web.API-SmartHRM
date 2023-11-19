using HUG.CRUD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public UtilsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("Avatar")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> images)
        {
  
            var folderImage = "images\\avatar";
            var uploadFile = new UploadFile(_environment.WebRootPath);
            var resUploadImage = await uploadFile.UploadImage(images, folderImage);

            if (resUploadImage.Status != 200)
            {
                ModelState.AddModelError("", resUploadImage.StatusMessage);
                return StatusCode(resUploadImage.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(resUploadImage.Status, resUploadImage.StatusMessage);
        }
    }
}
