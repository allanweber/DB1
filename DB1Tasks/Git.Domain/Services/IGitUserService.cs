using Git.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Domain.Services
{
    public interface IGitUserService
    {
        Task<ICollection<GitUser>> GetAllUsers();

        Task<GitUserDetail> GetUser(string userName);
    }
}
