using BeymenCase.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeymenCase.Application.Interfaces
{
    public interface IConfigurationService
    {
        Task<List<AppDataDto>> GetAsync();
        Task<AppDataDto> GetByNameAsync(string name);
        Task DeleteAsync(string name);
        Task UpdateAsync(UpdateAppDataRequest request);
        Task SaveAsync(AppDataRequest request);
    }
}
