using RH.Domain.Core.Services;
using RH.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Services
{
    public interface IOpportunityService: IService
    {
        Task Delete(int id);
        Task<List<OpportunityDto>> GetAll();
        Task Insert(OpportunityInsertDto dto);
        Task Update(OpportunityDto dto);
    }
}
