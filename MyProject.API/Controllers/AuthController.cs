using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyProject.Application.Abstractions.Services;
using MyProject.Application.DTOs.AppUserDto;
using MyProject.Application.RequestParameters.Wrapper;
using MyProject.Domain.Entities.Identity;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TurboAuto.Persistence.Contexts;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TurboAutoDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TurboAutoDbContext context, IEmailSender emailSender, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
            this.configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
              
                if (user != null  && await _userManager.CheckPasswordAsync(user, model.Password.Trim()))
                {
                  
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

                    var token = new JwtSecurityToken(
                        issuer: "https://localhost:7288",
                        audience: "https://localhost:7288",
                        expires: DateTime.UtcNow.AddMonths(1),
                        claims: claims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                        );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                else
                {
                    ModelState.AddModelError("Error", "Email or Password is wrong");
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string OldPassword, string NewPassword)
        {
            AppUser currentUser = await _userManager.GetUserAsync(User);

            var checkPass = await _userManager.CheckPasswordAsync(currentUser, OldPassword);
            if (!checkPass) return BadRequest(new Response<object> { Message = "Your password is wrong" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);


            var result = await _userManager.ResetPasswordAsync(currentUser, token, NewPassword);
            if (result.Succeeded)
            {
                return Ok(new Response<object>() { Message = "Password is updated" });
            }
            return BadRequest(new Response<object>() { Succeeded = false, Message = "An error occurred while renewing the password" });
        }

      


    }
}
