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
    public class CandidateService : ICandidateService
    {
        public CandidateService(IMapper mapper, ICandidateRepository candidateRepository)
        {
            Mapper = mapper;
            Repository = candidateRepository;
        }

        public IMapper Mapper { get; }
        public ICandidateRepository Repository { get; }

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

        public async Task<List<CandidateDto>> GetAll()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<Candidate>, List<CandidateDto>>(entity);

            return dto;
        }

        public async Task Insert(CandidateInsertDto dto)
        {
            var entity = this.Mapper.Map<CandidateInsertDto, Candidate>(dto);

            this.validate(entity);

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();
        }

        public async Task Update(CandidateDto dto)
        {
            var entity = this.Mapper.Map<CandidateDto, Candidate>(dto);

            this.validate(entity);

            await this.Repository.UpdateAsync(entity);

            await this.Repository.CommitAsync();
        }

        private void validate(Candidate opportunity)
        {
            if (string.IsNullOrEmpty(opportunity.Name))
                throw new Exception("Nome deve ser informado");

            if (opportunity.Name.Length > 150)
                throw new Exception("Nome não pode ser maior que 100 caracteres");
        }
    }
}
