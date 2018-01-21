using AutoMapper;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using RH.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RH.Infrastructure.Services
{
    public class OpportunityService: IOpportunityService
    {
        public OpportunityService(IMapper mapper, IOpportunityRepository opportunityRepository)
        {
            Mapper = mapper;
            Repository = opportunityRepository;
        }

        public IMapper Mapper { get; }
        public IOpportunityRepository Repository { get; }

        public async Task Delete(int id)
        {
            var entity = await this.Repository.GetAsync(id);

            if (entity == null)
            {
                throw new Exception("Oportunidade não encontrada.");
            }

            await this.Repository.DeleteAsync(entity);

            await this.Repository.CommitAsync();
        }

        public async Task<List<OpportunityDto>> GetAll()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<Opportunity>, List<OpportunityDto>>(entity);

            return dto;
        }

        public async Task Insert(OpportunityInsertDto dto)
        {
            var entity = this.Mapper.Map<OpportunityInsertDto, Opportunity>(dto);

            this.validate(entity);

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();
        }

        public async Task Update(OpportunityDto dto)
        {
            var entity = this.Mapper.Map<OpportunityDto, Opportunity>(dto);

            this.validate(entity);

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();
        }

        private void validate(Opportunity opportunity)
        {
            if (string.IsNullOrEmpty(opportunity.Name))
                throw new Exception("Nome deve ser informado");

            if (opportunity.Name.Length > 100)
                throw new Exception("Nome não pode ser maior que 100 caracteres");
        }
    }
}
