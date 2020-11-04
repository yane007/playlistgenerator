﻿using System;
using System.Collections.Generic;
using System.Text;
using PG.Models;

namespace PG.Services.DTOs
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool Is_loved_track { get; set; }
        public bool Collaborative { get; set; }
        public int Nb_tracks { get; set; }
        public int Fans { get; set; }
        public string Link { get; set; }
        public string Share { get; set; }
        public string picture { get; set; }
        public string Picture_small { get; set; }
        public string Picture_medium { get; set; }
        public string Picture_big { get; set; }
        public string Picture_xl { get; set; }
        public string Checksum { get; set; }
        public string Tracklist { get; set; }
        public string Creation_date { get; set; }
        public string Md5_image { get; set; }
        public string Type { get; set; }
        public int CreatorId { get; set; }
        public int UserId { get; set; }

    }
}
