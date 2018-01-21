using RH.Domain.Core.Services;
using RH.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Services
{
    public interface ITechnologyService: IService
    {
        Task Delete(int id);
        Task<List<TechnologyDto>> GetAll();
        Task Insert(TechnologyInsertDto dto);
        Task Update(TechnologyDto dto);
    }
}
