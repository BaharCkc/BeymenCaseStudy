using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeymenCase.Library.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetValueAsync<T>(string key);
        Task<bool> SetValueAsync<T>(string key, T data);
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
        T GetOrAdd<T>(string key, Func<T> action) where T : class;
        Task Clear(string key);
        void ClearAll();
    }
}
