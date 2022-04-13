using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Timelog.Api.Settings;
using Timelog.AspNetCore.CommandRequests;
using Timelog.AspNetCore.Models;
using Timelog.AspNetCore.ViewModels;

namespace MyMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;      
       
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            
            UserManager<User> userManager,           
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;            
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginRequest userLoginRequest)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginRequest.Email);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginRequest.Password);

            if (userSigninResult)
            {
                //var roles = await _userManager.GetRolesAsync(user);
                return Ok(GenerateJwt(user));
            }

            return BadRequest("Email or password incorrect.");
        }
       
        private string GenerateJwt(User user/*, IList<string> roles*/)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            //var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            //claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}