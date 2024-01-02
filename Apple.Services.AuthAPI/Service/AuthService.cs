using Apple.Services.AuthAPI.Data;
using Apple.Services.AuthAPI.Models;
using Apple.Services.AuthAPI.Models.Dto;
using Apple.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Apple.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator  )
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // Create Role if it does not Exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
                
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDTO requestDTO)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == requestDTO.UserName.ToLower());
           bool isValid = await _userManager.CheckPasswordAsync(user, requestDTO.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }
            // If User is found , Generate JWT Token
            var tokenGenerated = _jwtTokenGenerator.GenerateToken(user);

                UserDTO userDto = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    NameOfUser = user.NameOfUser,
                    PhoneNumber = user.PhoneNumber,
                    CreatedUser = DateTime.UtcNow
                };

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    User = userDto,
                    Token = tokenGenerated
                };

                return loginResponseDto;
            
        }

        public async Task<string> Register(RegistrationRequestDTO registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                NameOfUser = registrationRequestDto.NameOfUser,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                CreatedUser = DateTime.UtcNow
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    var userToReturn = _appDbContext.ApplicationUsers.First(u=>u.UserName == registrationRequestDto.Email);
                    
                    UserDTO userDto = new()
                    {
                         Email= userToReturn.Email,
                         ID = userToReturn.Id,
                         NameOfUser = userToReturn.NameOfUser,
                         PhoneNumber = userToReturn.PhoneNumber,
                         CreatedUser = DateTime.UtcNow
                    };
                    return " ";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

               
            }
            return "Error encountered";
        }

    }
}
