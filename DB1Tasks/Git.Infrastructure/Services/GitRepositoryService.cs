using Git.Domain.Services;
using Git.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services
{
    public class GitRepositoryService : IGitRepositoryService
    {
        public Task<List<GitRepository>> GetUserRepositories(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
