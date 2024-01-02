using Microsoft.AspNetCore.Mvc;
using XoperoRecruitmentTask.Client;
using XoperoRecruitmentTask.Server.Interfaces;

namespace XoperoRecruitmentTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IVolumeService _volumeService;

        public ReportController(IVolumeService volumeService)
        {
            _volumeService = volumeService;
        }

        [HttpPost("SaveData")]
        public async Task<IActionResult> SaveData([FromBody] VolumeRequest request)
        {
            var connectionId = Request.Headers["connectionId"];

            return Ok(await _volumeService.HandleRequest(request, connectionId));
        }
    }
}
