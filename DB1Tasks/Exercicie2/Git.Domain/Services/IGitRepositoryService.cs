using Git.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Domain.Services
{
    public interface IGitRepositoryService
    {
        Task<ICollection<Repository>> GetUserRepositories(string userName);
    }
}
