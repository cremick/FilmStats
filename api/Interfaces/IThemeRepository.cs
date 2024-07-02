using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IThemeRepository
    {
        Task<List<Theme>> GetAllAsync(ThemeQueryObject query);
    }
}