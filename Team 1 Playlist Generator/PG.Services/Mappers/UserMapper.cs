using PG.Models;
using PG.Services.DTOs;

namespace PG.Services.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = user.Token,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
            };
        }
        public static User ToEntity(this UserDTO userDTO)
        {
            return new User()
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                Token = userDTO.Token,
                LockoutEnd = userDTO.LockoutEnd,
            };
        }
    }
}
