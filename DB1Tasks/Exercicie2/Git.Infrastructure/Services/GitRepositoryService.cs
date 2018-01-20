using AutoMapper;
using Git.Domain.Dtos;
using Git.Domain.Services;
using Git.Domain.ValueObjects;
using Git.Infrastructure.Services.Facades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services
{
    public class GitRepositoryService : IGitRepositoryService
    {
        public GitRepositoryService(GitHubFacadeService gitHubFacadeService, IMapper mapper)
        {
            this.GitHubFacadeService = gitHubFacadeService;
            this.Mapper = mapper;
        }

        public GitHubFacadeService GitHubFacadeService { get; }
        public IMapper Mapper { get; }

        public async Task<ICollection<Repository>> GetUserRepositories(string userName)
        {
            var reposGit = await this.GitHubFacadeService.GetUserRepositories(userName);

            var repos = this.Mapper.Map<ICollection<GitRepository>, ICollection<Repository>>(reposGit);

            return repos;
        }
    }
}
