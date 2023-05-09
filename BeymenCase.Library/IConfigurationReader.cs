using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using BeymenCase.Library.Entities;

namespace BeymenCase.Library
{
    public interface IConfigurationReader
    {
        Task<T> GetValue<T>(string key);
    }
}
