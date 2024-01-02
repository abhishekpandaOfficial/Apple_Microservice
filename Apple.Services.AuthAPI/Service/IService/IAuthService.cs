using Apple.Services.AuthAPI.Models.Dto;

namespace Apple.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO requestDTO);
        Task<LoginResponseDto> Login(LoginRequestDTO requestDTO);
        Task<bool> AssignRole(string email, string roleName);
    }
}
