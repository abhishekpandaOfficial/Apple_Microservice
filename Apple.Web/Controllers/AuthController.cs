using Apple.Web.Models;
using Apple.Web.Service.IService;
using Apple.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Apple.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpGet]
        public IActionResult Register()
        {

            var roleLists = new List<SelectListItem>()
            {
                new SelectListItem{ Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{ Text=SD.RoleCustomer, Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roleLists;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestobj)
        {
            ResponseDto responseDto = await _authService.RegisterAsync(registrationRequestobj);
            ResponseDto assignRole;
            
            if (responseDto != null && responseDto.IsSuccess)
            {
                if (string.IsNullOrEmpty(registrationRequestobj.Role))
                {
                    registrationRequestobj.Role = SD.RoleCustomer;
                }
                
                assignRole = await _authService.AssignRoleAsync(registrationRequestobj);
               
                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration is Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
           
                var roleLists = new List<SelectListItem>()
                {
                new SelectListItem{ Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{ Text=SD.RoleCustomer, Value=SD.RoleCustomer}
                };
                ViewBag.RoleList = roleLists;
                TempData["error"] = "Please check your Password as it should be Alphanumeric & morethan 8 Characters";


            return View(registrationRequestobj);
        }


        [HttpGet]
        public IActionResult Logout()
        {

            return View();
        }
    }
}
