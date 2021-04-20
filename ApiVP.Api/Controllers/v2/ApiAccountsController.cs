using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiVP.Repository.Security;
using System;
using System.Text;

namespace ApiVP.Api.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ApiAccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public ApiAccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        private UserToken BuildToken(UserInfo userInfo, IList<string> roles)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(5.0);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo userInfo)
        {
            var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
            var result = await userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                return BuildToken(userInfo, new List<string>());
            }
            else
            {
                return BadRequest("usuario o password inválido");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> LogInUser([FromBody] UserInfo userInfo)
        {
            var result = await signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuario = await userManager.FindByEmailAsync(userInfo.Email);
                var roles = await userManager.GetRolesAsync(usuario);
                return BuildToken(userInfo, roles);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Intento de Log-In inválido");
                return BadRequest(ModelState);
            }
        }

    }
}