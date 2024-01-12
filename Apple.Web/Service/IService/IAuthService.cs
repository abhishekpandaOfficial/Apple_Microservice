using Apple.Web.Models;

namespace Apple.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDTO registrationRequestDtO);
        Task<ResponseDto> AssignRoleAsync(RegistrationRequestDTO  registrationRequestDtO);

    }
}
