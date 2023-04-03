using System.ComponentModel.DataAnnotations;

namespace DOANV1.Entities.DTO.UserDto.RegisterDto
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }


        public string UserName { get; set; }


        [Required(ErrorMessage = "Fullname is a required field.")]
        public string Fullname { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }


        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = " Password do not match")]
        public string ConfirmPassword { get; set; }




    }
}
