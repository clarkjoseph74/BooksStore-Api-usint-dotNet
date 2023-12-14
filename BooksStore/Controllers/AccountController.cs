using BooksStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public AccountController(UserManager<User> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(RegisterDTO user)
        {

            if (ModelState.IsValid)
            {
                User u = new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(u, user.Password);
                if (result.Succeeded)
                {
                    return Ok(new { user.UserName , user.Email});
                }
                else
                {
                    foreach ( var item in result.Errors )
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginCreds)
        {
            User? user = await _userManager.FindByNameAsync(loginCreds.UserName);
            if(user != null)
            {
                if(await _userManager.CheckPasswordAsync(user , loginCreds.Password))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name , user.UserName));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));   
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach( var role in roles )
                    {
                        claims.Add(new Claim(ClaimTypes.Role , role.ToString()));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SekretKey"]));
                    var signingCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: configuration["JWT:Issuer"],
                        audience: configuration["JWT:Audience"],
                        expires:DateTime.Now.AddDays(3),
                        signingCredentials: signingCreds
                        );

                    var _token = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                    };
                    return Ok(_token);    
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                ModelState.AddModelError("", "User Name is invalid");
            }
            return BadRequest(ModelState);
        }
    }
}
