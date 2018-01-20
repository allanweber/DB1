using Git.Domain.Constants;
using Git.Domain.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Git.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class RepositoryController: Controller
    {
        public IGitRepositoryService GitRepositoryService { get; }

        public RepositoryController(IGitRepositoryService gitRepositoryService)
        {
            this.GitRepositoryService = gitRepositoryService;
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<IActionResult> GetUserRepositories(string userName)
        {
            var repos = await this.GitRepositoryService.GetUserRepositories(userName);

            return this.Ok(repos);
        }
    }
}
