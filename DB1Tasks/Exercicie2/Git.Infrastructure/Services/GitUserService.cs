using AutoMapper;
using Git.Domain.Dtos;
using Git.Domain.Services;
using Git.Domain.ValueObjects;
using Git.Infrastructure.Services.Facades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services
{
    public class GitUserService : IGitUserService
    {
        public GitUserService(GitHubFacadeService gitHubFacadeService, IMapper mapper)
        {
            this.GitHubFacadeService = gitHubFacadeService;
            this.Mapper = mapper;
        }

        public GitHubFacadeService GitHubFacadeService { get; }
        public IMapper Mapper { get; }

        public async Task<ICollection<User>> GetAllUsers()
        {
            var usersGit = await this.GitHubFacadeService.GetAllUsers();

            var users = this.Mapper.Map<ICollection<GitUser>, ICollection<User>>(usersGit);

            return users;
        }

        public async Task<UserDetail> GetUser(string userName)
        {
            var userGit = await this.GitHubFacadeService.GetUser(userName);

            var user = this.Mapper.Map<GitUserDetail, UserDetail>(userGit);

            return user;
        }
    }
}
