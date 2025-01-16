using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RestaurantApi.Controllers
{
    [Route("file")]
    [Authorize]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            string rootPath = Directory.GetCurrentDirectory();

            string filePath = $"{rootPath}/PrivateFiles/{fileName}";

            bool fileExist = System.IO.File.Exists(filePath);
            if (!fileExist)
            {
                return NotFound();
            }

            byte[] fileContents = System.IO.File.ReadAllBytes(filePath);

            FileExtensionContentTypeProvider contensProvider = new FileExtensionContentTypeProvider();
            contensProvider.TryGetContentType(filePath, out string contentType);

            return File(fileContents, contentType, fileName);
        }
    }
}
