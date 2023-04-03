namespace DOANV1.Entities.DTO.UserDto.LoginDto
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
