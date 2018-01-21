using AutoMapper;
using RH.Domain.Core.Dtos;
using RH.Domain.Core.Services;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using RH.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Infrastructure.Services
{
    public class TechnologyService : ITechnologyService
    {
        public TechnologyService(IMapper mapper, ITechnologyRepository technologyRepository)
        {
            this.Mapper = mapper;
            this.Repository = technologyRepository;
        }

        public IMapper Mapper { get; }
        public ITechnologyRepository Repository { get; }

        public async Task Delete(int id)
        {
            var entity = await this.Repository.GetAsync(id);

            if(entity == null)
            {
                throw new Exception("Tecnologia não encontrada.");
            }

            await this.Repository.DeleteAsync(entity);

            await this.Repository.CommitAsync();
        }

        public async Task<List<TechnologyDto>> GetAll()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<Technology>, List<TechnologyDto>>(entity);

            return dto;
        }

        public async Task Insert(TechnologyInsertDto dto)
        {
            var entity = this.Mapper.Map<TechnologyInsertDto, Technology>(dto);

            this.validate(entity);

            await this.Repository.InsertAsync(entity);

            await this.Repository.CommitAsync();
        }

        public async Task Update(TechnologyDto dto)
        {
            var tech = this.Mapper.Map<TechnologyDto, Technology>(dto);

            this.validate(tech);

            await this.Repository.UpdateAsync(tech);

            await this.Repository.CommitAsync();
        }

        private void validate(Technology tech)
        {
            if (string.IsNullOrEmpty(tech.Name))
                throw new Exception("Nome deve ser informado");

            if (tech.Name.Length > 100)
                throw new Exception("Nome não pode ser maior que 100 caracteres");
        }
    }
}
