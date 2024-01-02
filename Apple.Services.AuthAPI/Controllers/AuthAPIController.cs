using Apple.Services.AuthAPI.Data;
using Apple.Services.AuthAPI.Models;
using Apple.Services.AuthAPI.Models.Dto;
using Apple.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Apple.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
       
        private readonly IAuthService _authService;
        private ResponseDto _responseDto;
        private ILogger<ApplicationUser> _Authlogger;
        public AuthAPIController(IAuthService authService, ILogger<ApplicationUser> Authlogger)
        {
            
            _authService = authService;
            _Authlogger = Authlogger;
            _responseDto = new();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            _Authlogger.LogInformation("AuthAPI Controller with Register & Post method- started at {date}", DateTime.UtcNow);
            var errorMessage = await _authService.Register(model);  
            if (!string.IsNullOrEmpty(errorMessage)) 
            
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                _Authlogger.LogInformation("AuthAPI Controller with Badrequest at {date}", DateTime.UtcNow);
                return BadRequest(_responseDto);
                
            }

            _Authlogger.LogInformation("AuthAPI Controller with Registration Successful at {date} and the User Name is  " + model.NameOfUser + "and Created at " + DateTime.UtcNow);
            return Ok(_responseDto);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto)
        {
            _Authlogger.LogInformation("AuthAPI Controller with Login  & Post method- started at {date}", DateTime.UtcNow);

            var loginResponse = await _authService.Login(loginRequestDto);
            if(loginResponse.User == null)
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message = "UserName or Password in Invalid";
                _Authlogger.LogInformation("AuthAPI Controller with Badrequest at {date}", DateTime.UtcNow + "and it is Bad Request");
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper() );
            if (!assignRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
