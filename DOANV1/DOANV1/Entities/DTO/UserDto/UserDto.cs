using System;

namespace DOANV1.Entities.DTO.UserDto
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
