﻿using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Services
{
    public class ArtistService : IArtistService
    {
        private readonly PGDbContext _context;

        public ArtistService(PGDbContext context)
        {
            _context = context;
        }


        public async Task<ArtistDTO> Create(ArtistDTO artistDTO)
        {
            if (artistDTO == null)
            {
                throw new ArgumentNullException("Null Artist");
            }
            if (artistDTO.Name.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Artist's Name needs to be shorter than 100 characters.");
            }

            var existedArtist = await _context.Artists.FirstOrDefaultAsync(x => x.Name == artistDTO.Name);
            if (existedArtist != null)
            {
                throw new ArgumentException($"Artist with name '{artistDTO.Name}' already exists.");
            }

            var artist = await _context.Artists.AddAsync(artistDTO.ToEntity());
            await _context.SaveChangesAsync();

            return artist.Entity.ToDTO();
        }

        public async Task Delete(int id)
        {
            var expectedArtist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedArtist == null)
            {
                throw new ArgumentNullException($"Artist with id {id} was not found.");
            }
            if (expectedArtist.IsDeleted)
            {
                throw new ArgumentException($"Artist with id {id} is already deleted.");
            }

            expectedArtist.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ArtistDTO>> GetAllArtists()
        {
            return await _context.Artists.Where(x => x.IsDeleted == false)
                                        .Select(x => x.ToDTO())
                                        .ToListAsync();
        }

        public async Task<ArtistDTO> GetArtistById(int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (artist == null)
            {
                throw new ArgumentNullException($"Artist with id {id} was not found.");
            }

            return artist.ToDTO();
        }

        public async Task<ArtistDTO> Update(int id, ArtistDTO artistDTO)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (artist == null)
            {
                throw new ArgumentNullException($"Artist with id {id} was not found.");
            }

            artist.Name = artistDTO.Name;
            artist.Tracklist = artistDTO.Tracklist;
            artist.Type = artistDTO.Type;

            await _context.SaveChangesAsync();

            return artist.ToDTO();
        }
    }
}