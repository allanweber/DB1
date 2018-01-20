using Git.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Domain.Services
{
    public interface IGitUserService
    {
        Task<ICollection<User>> GetAllUsers();

        Task<UserDetail> GetUser(string userName);
    }
}
