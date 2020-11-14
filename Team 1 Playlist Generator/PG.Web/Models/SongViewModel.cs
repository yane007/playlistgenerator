using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }
    }
}