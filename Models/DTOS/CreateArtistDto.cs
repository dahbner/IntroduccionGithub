using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public record CreateArtistDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }
    }
}