

using DOANV1.Entities.DTO.UserDto.LoginDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DOANV1.Interface.IUser
{

    public interface ILogin_JWT
    {
      
        Task<bool> ValidateUser(LoginRequestDto loginRequest);
        Task<string> CreateToken();
    }

}
