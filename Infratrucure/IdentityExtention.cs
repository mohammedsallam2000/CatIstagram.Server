using System.Linq;
using System.Security.Claims;

namespace CatIstagram.Server.Infratrucure
{
    public static class IdentityExtention
    {
        public static string GetId(this ClaimsPrincipal user)
            => user
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
            .Value;
    }
}
