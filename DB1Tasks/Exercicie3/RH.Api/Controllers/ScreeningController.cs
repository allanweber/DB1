using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RH.Domain.Constants;
using RH.Domain.Services;
using System.Threading.Tasks;

namespace RH.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class ScreeningController: Controller
    {
        public ScreeningController(IScreeningService screeningService)
        {
            ScreeningService = screeningService;
        }

        public IScreeningService ScreeningService { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var screening = await this.ScreeningService.SortCandidates();

            return Ok(screening);
        }
    }
}
