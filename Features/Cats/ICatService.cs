using System.Threading.Tasks;

namespace CatIstagram.Server.Features.Cats
{
    public interface ICatService
    {
        public Task<int> Create(CreateCatRequestModel model, string userId);
    }
}
