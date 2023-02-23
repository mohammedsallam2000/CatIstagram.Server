namespace CatIstagram.Server.Features.Idintity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string UserId, string UserName, string secret);
    }
}