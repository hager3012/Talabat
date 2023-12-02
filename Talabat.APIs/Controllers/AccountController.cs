using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Servicies.Contract;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthServices _authServices;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IAuthServices authServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = authServices;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto userLogin)
        {
            var user =await _userManager.FindByEmailAsync(userLogin.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user, _userManager)
            }); ;

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(userRegisterDto userRegister) 
        {
            var user = new AppUser()
            {
                Email = userRegister.Email,
                Name = userRegister.Name,
                UserName = userRegister.Email.Split("@")[0],
                PhoneNumber = userRegister.PhoneNumber,
                PasswordHash = userRegister.Password
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user, _userManager)
            });
        }
    }
}
