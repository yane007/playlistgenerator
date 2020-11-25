using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public bool LockoutEnabled { get; set; }

        public string Name { get; set; }

        public string Token  { get; set; }
    }
}
