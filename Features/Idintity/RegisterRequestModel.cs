using System.ComponentModel.DataAnnotations;

namespace CatIstagram.Server.Features.Idintity
{
    public class RegisterRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
