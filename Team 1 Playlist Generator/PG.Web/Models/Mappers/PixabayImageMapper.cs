using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models.Mappers
{
    public static class PixabayImageMapper
    {
        public static PixabayImageDTO ToDTO(this PixabayImageViewModel image)
        {
            return new PixabayImageDTO()
            {
                Id = image.Id,
                LargeImageURL = image.LargeImageURL,
                WebformatURL = image.WebformatURL,
                PreviewURL = image.PreviewURL,
            };
        }

        public static PixabayImageViewModel ToViewModel(this PixabayImageDTO image)
        {
            return new PixabayImageViewModel()
            {
                Id = image.Id,
                LargeImageURL = image.LargeImageURL,
                WebformatURL = image.WebformatURL,
                PreviewURL = image.PreviewURL,
            };
        }
    }
}
