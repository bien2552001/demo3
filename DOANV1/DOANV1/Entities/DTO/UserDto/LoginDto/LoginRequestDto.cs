using System.ComponentModel.DataAnnotations;

namespace DOANV1.Entities.DTO.UserDto.LoginDto
{
    public class LoginRequestDto
    {

        [Required, EmailAddress]
        public string Email { get; set; }



        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
