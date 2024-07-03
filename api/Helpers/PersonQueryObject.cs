using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.Helpers
{
    public class PersonQueryObject
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}