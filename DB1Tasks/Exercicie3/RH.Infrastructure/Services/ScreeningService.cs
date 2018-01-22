using RH.Domain.Dtos;
using RH.Domain.Repositories;
using RH.Domain.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RH.Domain.Entities;

namespace RH.Infrastructure.Services
{
    public class ScreeningService : IScreeningService
    {
        public ScreeningService(
            IMapper mapper,
            IOpportunityRepository opportunityRepository,
            IOpportunityTechRepository opportunityTechRepository,
            ICandidateTechRepository candidateTechRepository)
        {
            this.Mapper = mapper;
            this.OpportunityRepository = opportunityRepository;
            this.OpportunityTechRepository = opportunityTechRepository;
            this.CandidateTechRepository = candidateTechRepository;
        }

        public IMapper Mapper { get; }
        public IOpportunityRepository OpportunityRepository { get; }
        public IOpportunityTechRepository OpportunityTechRepository { get; }
        public ICandidateTechRepository CandidateTechRepository { get; }

        public async Task<List<ScreeningDto>> SortCandidates()
        {
            var opportunities = await this.OpportunityRepository.GetAllAsync();

            List<ScreeningDto> screening = new List<ScreeningDto>();
            ScreeningDto currentDto = null;
            List<CandidateTech> candidatesTechs;
            CandidateScreeningDto candidateDto;

            foreach (var opportunity in opportunities)
            {
                currentDto = new ScreeningDto { Opportunity = this.Mapper.Map<Opportunity, OpportunityScreeningDto>(opportunity) };
                currentDto.Opportunity.Technologies = await this.OpportunityTechRepository.GetAllTechnologyByOpportunity(opportunity.Id);

                foreach (var tech in currentDto.Opportunity.Technologies)
                {
                    candidatesTechs = await this.CandidateTechRepository.GetByTechnology(tech.Id);

                    foreach (var candidateTech in candidatesTechs)
                    {
                        if (!currentDto.Candidates.Any(c => c.Id == candidateTech.Candidate.Id))
                        {
                            candidateDto = this.Mapper.Map<Candidate, CandidateScreeningDto>(candidateTech.Candidate);
                            candidateDto.Technologies = await this.CandidateTechRepository.GetAllTechnologyByCandidate(candidateTech.Candidate.Id);

                            currentDto.Candidates.Add(candidateDto);
                        }
                    }
                }

                screening.Add(currentDto);
            }


            return screening;
        }
    }
}
