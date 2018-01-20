using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Git.Domain.Constants;
using Git.Domain.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Git.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class UserController : Controller
    {
        public IGitUserService GitUserService { get; }

        public UserController(IGitUserService gitUserService)
        {
            this.GitUserService = gitUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users  = await this.GitUserService.GetAllUsers();

            return this.Ok(users);
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await this.GitUserService.GetUser(userName);

            return this.Ok(user);
        }
    }
}
