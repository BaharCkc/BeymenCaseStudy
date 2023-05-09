using BeymenCase.Library.Entities;
using BeymenCase.Library.Interfaces;
using BeymenCase.Library.Repositories;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BeymenCase.Library
{
    public class ConfigurationReader : IConfigurationReader
    {
        private string applicationName;
        private int refreshTimerIntervalInMS;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ICacheService _cacheService;
        private readonly IConfigRepository _configRepository;
        private readonly Timer _timer;
        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMS)
        {
            this.applicationName = applicationName;
            this.refreshTimerIntervalInMS = refreshTimerIntervalInMS;

            _configRepository = new ConfigRepository(connectionString);
            _connectionMultiplexer = ConnectionMultiplexer.Connect("redis:6379,abortConnect=false");
            _timer = new Timer(OnTimerElapsed, null, refreshTimerIntervalInMS, refreshTimerIntervalInMS);

        }
        public async Task<T> GetValue<T>(string key)
        {
            var cacheKey = $"{applicationName}_{key}";
            var readData = await _cacheService.GetValueAsync<AppData>(cacheKey);

            if (readData is null)
            {
                var appData = await _configRepository.GetAppDataAsync(applicationName, key);
                await _cacheService.SetValueAsync(cacheKey, appData);

                return (T)Convert.ChangeType(appData.Value, typeof(T));
            }

            return (T)Convert.ChangeType(readData.Value, typeof(T));
        }
        private async void OnTimerElapsed(object state)
        {
            await RefreshData();
        }
        private async Task RefreshData()
        {
            var list = await _configRepository.GetAppDataListAsync(applicationName);
            foreach (var item in list)
            {
                var cacheKey = $"{item.ApplicationName}_{item.Name}";
                var result = await _cacheService.GetValueAsync<AppData>(cacheKey);
                if (result == null)
                {
                    await _cacheService.SetValueAsync(cacheKey, item);
                    return;
                }

                if (result.Value != item.Value || result.Type != item.Type)
                {
                    await _cacheService.Clear(cacheKey);
                    await _cacheService.SetValueAsync(cacheKey, item);
                }
            }
        }

    }
}