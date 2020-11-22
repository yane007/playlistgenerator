using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class PixabayImageViewModel
    {
        public int Id { get; set; }

        public string PreviewURL { get; set; }

        public string WebformatURL { get; set; }

        public string LargeImageURL { get; set; }
    }
}
