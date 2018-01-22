using AutoMapper;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Dtos;
using RH.Domain.Entities;

namespace RH.Infrastructure.Mappers
{
    public class DtoToEntities : Profile
    {
        public DtoToEntities()
        {
            this.CreateMap<CandidateInsertCommand, Candidate>();
            this.CreateMap<CandidateUpdateCommand, Candidate>();

            this.CreateMap<OpportunityInsertCommand, Opportunity>();
            this.CreateMap<OpportunityUpdateCommand, Opportunity>();

            this.CreateMap<TechnologyInsertCommand, Technology>();
            this.CreateMap<TechnologyUpdateCommand, Technology>();

            this.CreateMap<CandidateTechInsertCommand, CandidateTech>();
            this.CreateMap<CandidateTechUpdateCommand, CandidateTech>();

            this.CreateMap<OpportunityTechInsertCommand, OpportunityTech>();
            this.CreateMap<OpportunityTechUpdateCommand, OpportunityTech>();

            this.CreateMap<OpportunityCandidateInsertCommand, OpportunityCandidate>();
            this.CreateMap<OpportunityCandidateUpdateCommand, OpportunityCandidate>();
        }
    }
}
