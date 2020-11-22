﻿using PG.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PG.Models
{
    public class Playlist : Entity
    {
        public Playlist()
        {
            PlaylistsSongs = new List<PlaylistsSongs>();
            PlaylistsGenres = new List<PlaylistsGenres>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public int PixabayId { get; set; }
        public PixabayImage PixabayImage { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; }

        public ICollection<PlaylistsGenres> PlaylistsGenres { get; set; }
    }
}