using Git.Domain.Services;
using Git.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services
{
    public class GitUserService : IGitUserService
    {
        public async Task<ICollection<GitUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<GitUserDetail> GetUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
