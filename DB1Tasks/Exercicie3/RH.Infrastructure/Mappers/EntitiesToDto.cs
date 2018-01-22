using AutoMapper;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using System.Linq;

namespace RH.Infrastructure.Mappers
{
    public class EntitiesToDto : Profile
    {
        public EntitiesToDto()
        {
            this.CreateMap<Technology, TechnologyDto>();
            this.CreateMap<Opportunity, OpportunityDto>();
            this.CreateMap<Candidate, CandidateDto>();

            this.CreateMap<CandidateTech, CandidateTechDto>()
                .ForMember(to => to.CandidateName, source => source.MapFrom(from => from.Candidate.Name))
                .ForMember(to => to.TechnologyName, source => source.MapFrom(from => from.Technology.Name));

            this.CreateMap<OpportunityTech, OpportunityTechDto>()
                .ForMember(to => to.OpportunityName, source => source.MapFrom(from => from.Opportunity.Name))
                .ForMember(to => to.TechnologyName, source => source.MapFrom(from => from.Technology.Name));

            this.CreateMap<OpportunityCandidate, OpportunityCandidateDto>()
                .ForMember(to => to.OpportunityName, source => source.MapFrom(from => from.Opportunity.Name))
                .ForMember(to => to.CandidateName, source => source.MapFrom(from => from.Candidate.Name));

            this.CreateMap<Candidate, CandidateScreeningDto>();

            this.CreateMap<Opportunity, OpportunityScreeningDto>();
        }
    }
}
