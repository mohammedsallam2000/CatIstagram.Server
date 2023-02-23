using CatIstagram.Server.Data.Entites;
using CatIstagram.Server.Features.Idintity;
using Microsoft.AspNetCore.Http;
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

namespace CatIstagram.Server.Controllers.Idintity
{

    public class IdentityController : ApiController
    {
        private readonly UserManager<user> userManager;
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController(UserManager<user> UserManager, IOptions<AppSettings> appSettings,IIdentityService identityService)
        {
            userManager = UserManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]

        [Route(nameof(Register))]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new user
            {
                Email = model.Email,
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //return this.StatusCode(HttpStatusCode.Created)
                return Ok();

            }
            return BadRequest(result.Errors);
        }
        [HttpPost]

        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized();

            }
           
            var token = this.identityService.GenerateJwtToken(user.Id,user.UserName,this.appSettings.secret);
            return new LoginResponseModel
            {
                token = token,
            };
        }
    }
}
