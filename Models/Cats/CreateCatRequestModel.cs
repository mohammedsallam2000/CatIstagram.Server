using CatIstagram.Server.Data.Entites;
using System.ComponentModel.DataAnnotations;
using static CatIstagram.Server.Data.Validation.cat;
namespace CatIstagram.Server.Data.Models.Cats
{
    public class CreateCatRequestModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public user User { get; set; }
    }
}
