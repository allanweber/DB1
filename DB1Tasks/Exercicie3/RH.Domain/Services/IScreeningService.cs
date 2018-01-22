using RH.Domain.Core.Services;
using RH.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Domain.Services
{
    public interface IScreeningService: IService
    {
        Task<List<ScreeningDto>> SortCandidates();
    }
}
