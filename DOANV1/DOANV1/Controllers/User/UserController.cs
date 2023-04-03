using AutoMapper;
using DOANV1.Entities.DTO.UserDto.LoginDto;
using DOANV1.Entities.DTO.UserDto.RegisterDto;
using DOANV1.Entities.DTO.UserDto.RolesDto;
using DOANV1.Entities.Model.User;
using DOANV1.Extensions.Service.ActionFilters;
using Interface.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DOANV1.Interface.IUser;
using System.Collections.Generic;

namespace DOANV1.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User_Model> _userManager;
        private readonly RoleManager<Role_Model> _roleManager;
        private readonly ILogin_JWT _login;

        public UserController(IMapper mapper, ILoggerService logger, UserManager<User_Model> userManager, RoleManager<Role_Model> roleManager, ILogin_JWT login)
        {
            _mapper = mapper;
            _logger= logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _login = login;
        }



        [HttpPost("role")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequestDto request)
        {
            var appRole = new Role_Model { Name = request.Role };

            await _roleManager.CreateAsync(appRole);

            return Ok(new { Message = "==========>>>>>>>>>>> CreateRoleRequestDto____Role created successfully", request.Role });

        }


        [HttpPost("role/register")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto register)
        {


            var checkEmail = await _userManager.FindByEmailAsync(register.Email);

            if (checkEmail != null) return BadRequest(" Email already exists, re-enter another email ");

            var user1 = _mapper.Map<User_Model>(register);


            var result = await _userManager.CreateAsync(user1, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            var map = _mapper.Map(user1, checkEmail);

            await _userManager.AddToRoleAsync(map, "USER");
           
            return Ok(new { Message = " ==========>>>>>>>>>>>   User register Successful ", register.Email, register.Password });

        }



        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!await _login.ValidateUser(request)) return Unauthorized("Authentication failed.Wrong user name or password.");

            return Ok(new { Token = await _login.CreateToken(), email = request.Email, password = request.Password, Message = " ==========>>>>>>>>>>> User Login Successful <<<<<<<<<<=========" });

        }



        //public async Task<IEnumerable<User_Model>> GetAllUsersAsync()
        //{
        //    return await _userManager.GetUserAsync().ToList();
        //}

        [HttpGet]
        public async Task<IEnumerable<IActionResult>> GetProfile()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return (IEnumerable<IActionResult>)NotFound(new { message = "User not found" });
            }
            _logger.LogInfo(" GET___User Successful");

            return (IEnumerable<IActionResult>)Ok();

        }







    }

}
