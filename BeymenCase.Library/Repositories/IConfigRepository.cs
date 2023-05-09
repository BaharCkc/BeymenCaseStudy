using BeymenCase.Library.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeymenCase.Library.Repositories
{
    public interface IConfigRepository
    {
        Task<AppData> GetAppDataAsync(string applicationName, string key);
        Task<List<AppData>> GetAppDataListAsync(string applicationName);
    }
}
