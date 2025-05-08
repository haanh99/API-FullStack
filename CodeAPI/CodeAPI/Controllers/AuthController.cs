using CodeAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }
        //POST: {apibaseUrl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            //Create Identity object
            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim()
            };
            // Create User
            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                // Add Role to user (reader)
                var addToResult = await _userManager.AddToRoleAsync(user, "Reader");
                if (addToResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (addToResult.Errors.Any())
                    {
                        foreach (var error in addToResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }

        //POST: {apibaseUrl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // CHECK USER EXIT EMAIL
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser != null)
            {
                //Check Password
                var chekingPassWord = await _userManager.CheckPasswordAsync(identityUser, request.Password);

                if (chekingPassWord)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    //Create a Token and Response
                    var response = new LoginResponseDto
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = "TOKEN"
                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");
            return ValidationProblem(ModelState);
        }
    }
}
