using CatIstagram.Server.Data;
using CatIstagram.Server.Data.Entites;
using CatIstagram.Server.Data.Models.Cats;
using CatIstagram.Server.Infratrucure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CatIstagram.Server.Controllers
{
    public class CatsController : ApiController
    {
        private readonly ApplicationDbContext data;

        public CatsController(ApplicationDbContext data)
        {
            this.data = data;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateCatRequestModel model)
        {

            var userId = this.User.GetId();
            var cat = new Cat
            {
                Description = model.Description,
                ImageUrl = model.Description,
                UserId = userId
            };

            this.data.Add(cat);
            await this.data.SaveChangesAsync();

            return Created(nameof(this.Create), cat.Id);
        }
    }
}
