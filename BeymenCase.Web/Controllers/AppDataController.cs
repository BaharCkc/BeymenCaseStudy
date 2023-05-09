using BeymenCase.Application.Interfaces;
using BeymenCase.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeymenCase.Web.Controllers
{
    [Route("api/appData")]
    [ApiController]
    public class AppDataController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public AppDataController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        [HttpGet]
        public async Task<List<AppDataDto>> GetAllAsync()
        {
            return await _configurationService.GetAsync();
        }
        [HttpGet("detail")]
        public async Task<AppDataDto> GetByNameAsync(string name)
        {
            return await _configurationService.GetByNameAsync(name);
        }
        [HttpDelete]
        public async Task DeleteAsync(string name)
        {
            await _configurationService.DeleteAsync(name);
        }
        [HttpPut]
        public async Task UpdateAsync(UpdateAppDataRequest request)
        {
            await _configurationService.UpdateAsync(request);
        }
        [HttpPost]
        public async Task SaveAsync(AppDataRequest request)
        {
            await _configurationService.SaveAsync(request);
        }
    }
}
