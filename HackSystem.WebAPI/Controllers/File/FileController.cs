using System.Threading.Tasks;
using HackSystem.WebAPI.Services.API.FileStores;
using Microsoft.AspNetCore.Mvc;
using IOFile = System.IO.File;

namespace HackSystem.WebAPI.Controllers.File
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IProgramFileStoreService programFileStoreService;

        public FileController(
            IProgramFileStoreService programFileStoreService)
        {
            this.programFileStoreService = programFileStoreService;
        }

        public async Task<IActionResult> ProgramIcon(string programId)
        {
            var path = this.programFileStoreService.GetProgramIconFile(programId);
            if (!IOFile.Exists(path)) path = "~/image/Icon/HackSystemIcon.png";
            return this.File(path, "image/png");
        }
    }
}
