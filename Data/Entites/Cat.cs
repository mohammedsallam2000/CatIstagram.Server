using System.ComponentModel.DataAnnotations;
using static CatIstagram.Server.Data.Validation.cat;

namespace CatIstagram.Server.Data.Entites
{
    public class Cat
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string UserId { get; set; }
        public user User { get; set; }
    }
}
