using System.ComponentModel.DataAnnotations;

namespace CatIstagram.Server.Features.Idintity
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
