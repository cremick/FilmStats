using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Theme;
using api.Models;

namespace api.Mappers
{
    public static class ThemeMappers
    {
        public static ThemeDto ToThemeDto(this Theme themeModel)
        {
            return new ThemeDto
            {
                Id = themeModel.Id,
                Title = themeModel.Title
            };
        }

        public static Theme ToThemeFromCreateDto(this CreateThemeDto themeDto)
        {
            return new Theme
            {
                Title = themeDto.Title
            };
        }
    }
}