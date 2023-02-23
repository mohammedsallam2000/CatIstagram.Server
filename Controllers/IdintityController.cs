using CatIstagram.Server.Data.Entites;
using CatIstagram.Server.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatIstagram.Server.Controllers
{

    public  class IdintityController : ApiController
    {
        private readonly UserManager<user> userManager;
        private readonly AppSettings appSettings;

        public IdintityController(UserManager<user> UserManager,IOptions<AppSettings> appSettings)
        {
            this.userManager = UserManager;
            this.appSettings = appSettings.Value;
        }
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new user
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await this.userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                //return this.StatusCode(HttpStatusCode.Created)
                return Ok();

            }
            return BadRequest(result.Errors);
        }
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();

            }
            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(this.appSettings.secret);
            var TokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var Token = TokenHandler.CreateToken(TokenDescription);
            var encryptedToken = TokenHandler.WriteToken(Token);
            return new JsonResult(encryptedToken) ;
        }
    }
}
