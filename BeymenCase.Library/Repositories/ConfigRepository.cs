using BeymenCase.Library.Entities;
using BeymenCase.Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeymenCase.Library.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConfigRepository(string connectionString)
        {
            _dbContext = new ApplicationDbContext(connectionString);
        }
        public async Task<AppData> GetAppDataAsync(string applicationName, string key)
        {
            return await _dbContext.AppData
                         .FirstOrDefaultAsync(b => b.ApplicationName == applicationName &&
                                                   b.IsActive && b.Name == key);
        }

        public async Task<List<AppData>> GetAppDataListAsync(string applicationName)
        {
            return await _dbContext.AppData
                       .Where(b => b.ApplicationName == applicationName && 
                                   b.IsActive)
                       .ToListAsync();
        }
    }
}
