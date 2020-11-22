using PG.Models;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Services.Mappers
{
    public static class PixabayImageMapper
    {
        public static PixabayImageDTO ToDTO(this PixabayImage image)
        {
            return new PixabayImageDTO()
            {
                Id = image.Id,
                LargeImageURL = image.LargeImageURL,
                WebformatURL = image.WebformatURL,
                PreviewURL = image.PreviewURL,
            };
        }
        public static PixabayImage ToEntity(this PixabayImageDTO imageDTO)
        {
            return new PixabayImage()
            {
                Id = imageDTO.Id,
                LargeImageURL = imageDTO.LargeImageURL,
                WebformatURL = imageDTO.WebformatURL,
                PreviewURL = imageDTO.PreviewURL,
            };
        }
    }
}
