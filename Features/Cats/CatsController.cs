using CatIstagram.Server.Controllers;
using CatIstagram.Server.Data;
using CatIstagram.Server.Data.Entites;
using CatIstagram.Server.Infratrucure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatIstagram.Server.Features.Cats
{
    public class CatsController : ApiController
    {
        private readonly ICatService catService;

        public CatsController(ICatService catService)
        {
            this.CatService = catService;
        }

        public ICatService CatService { get; }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {

            var userId = User.GetId();
           var catId =  await catService.Create(model, userId);
            return Created(nameof(this.Create), catId);
        }
    }
}
