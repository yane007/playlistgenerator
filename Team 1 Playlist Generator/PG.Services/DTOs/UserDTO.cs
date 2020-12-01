using System;

namespace PG.Services.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
