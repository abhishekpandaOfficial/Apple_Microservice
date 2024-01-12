
using Apple.Web.Models;
using Apple.Web.Service.IService;
using Apple.Web.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.AccessControl;
using System;

namespace Apple.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDtO)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.POST,
                Data = registrationRequestDtO,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.POST,
                Data = loginRequestDTO,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });

        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDTO registrationRequestDtO)
        {

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiTypeEnum.POST,
                Data = registrationRequestDtO,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
