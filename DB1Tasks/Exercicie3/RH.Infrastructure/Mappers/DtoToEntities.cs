using AutoMapper;
using RH.Domain.Dtos;
using RH.Domain.Entities;

namespace RH.Infrastructure.Mappers
{
    public class DtoToEntities : Profile
    {
        public DtoToEntities()
        {
            this.CreateMap<TechnologyInsertDto, Technology>();
            this.CreateMap<OpportunityInsertDto, Opportunity>();
            this.CreateMap<CandidateInsertDto, Candidate>();
            this.CreateMap<CandidateTechInsertDto, CandidateTech>();
            this.CreateMap<CandidateTechUpdateDto, CandidateTech>();
        }
    }
}
