using System.ComponentModel.DataAnnotations;

namespace DOANV1.Entities.DTO.UserDto.RolesDto
{
    public class RoleRequestDto
    {
        [Required(ErrorMessage = " Role is a required fiels")]
        public string Role { get; set; }
    }
}
