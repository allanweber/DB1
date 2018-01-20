using AutoMapper;
using Git.Domain.Dtos;
using Git.Domain.ValueObjects;

namespace Git.Infrastructure.Mappers
{
    public class ValueObjectToDto: Profile
    {
        public ValueObjectToDto()
        {
            this.CreateMap<GitUser, User>();
            this.CreateMap<GitUserDetail, UserDetail>();
            this.CreateMap<GitRepository, Repository>();
        }
    }
}
