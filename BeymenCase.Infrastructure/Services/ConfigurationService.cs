using BeymenCase.Application.Interfaces;
using BeymenCase.Application.Models;
using BeymenCase.Application.Repositories;
using BeymenCase.Library;
using BeymenCase.Library.Entities;
using BeymenCase.Library.Interfaces;
using BeymenCase.Library.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeymenCase.Infrastructure.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IRepository<AppData> _repository;

        public ConfigurationService(IRepository<AppData> repository)
        {
            _repository = repository;
        }

        public async Task DeleteAsync(string name)
        {
            var data = await _repository.GetByNameAsync(name);

            if (data is null)
            {
                return;
            }

            data.IsActive = false;

            await _repository.UpdateAsync(data);
        }

        public async Task<List<AppDataDto>> GetAsync()
        {
            var list = await _repository.GetAll().ToListAsync();

            if (!list.Any())
            {
                return new List<AppDataDto>();
            }

            return list.Adapt<List<AppDataDto>>();
        }

        public async Task<AppDataDto> GetByNameAsync(string name)
        {
            var data = await _repository.GetByNameAsync(name);

            return data.Adapt<AppDataDto>();
        }

        public async Task SaveAsync(AppDataRequest request)
        {
            var data = request.Adapt<AppData>();

            await _repository.AddAsync(data);
        }

        public async Task UpdateAsync(UpdateAppDataRequest request)
        {
            var data = await _repository.GetByNameAsync(request.Name);

            if (data is null)
            {
                return;
            }

            data.Name = request.Name;
            data.ApplicationName = request.ApplicationName;
            data.Value = request.Value;

            await _repository.UpdateAsync(data);
        }
    }
}
