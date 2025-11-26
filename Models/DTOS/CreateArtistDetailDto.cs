using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public record CreateArtistDetailDto
    {
        [Required(ErrorMessage = "Biography is required")]
        [MaxLength(2000)]
        public string Biography { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }

        public string? ManagerContact { get; set; }

        [Required]
        public Guid ArtistId { get; set; } 
    }
}