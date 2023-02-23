using CatIstagram.Server.Data;
using CatIstagram.Server.Data.Entites;
using System.Threading.Tasks;

namespace CatIstagram.Server.Features.Cats
{
    public class CatService : ICatService
    {
        private readonly ApplicationDbContext data;

        public CatService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public async Task<int> Create(CreateCatRequestModel model,string userId)
        {
            var cat = new Cat
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                UserId = userId
            };

            data.Add(cat);
            await data.SaveChangesAsync();
            return cat.Id;
        }
    }
}
