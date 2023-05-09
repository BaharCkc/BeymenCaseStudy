using System;

namespace BeymenCase.Application.Models
{
    public class UpdateAppDataRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
    }
}
