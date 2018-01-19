using Git.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Domain.Services
{
    public interface IGitRepositoryService
    {
        Task<List<GitRepository>> GetUserRepositories(string userName);
    }
}
