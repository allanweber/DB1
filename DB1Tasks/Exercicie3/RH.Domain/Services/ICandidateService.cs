using RH.Domain.Core.Services;
using RH.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Services
{
    public interface ICandidateService: IService
    {
        Task Delete(int id);
        Task<List<CandidateDto>> GetAll();
        Task Insert(CandidateInsertDto dto);
        Task Update(CandidateDto dto);
    }
}
