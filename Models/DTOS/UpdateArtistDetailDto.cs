using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public record UpdateArtistDetailDto
    {
        [Required]
        [MaxLength(2000)]
        public string Biography { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }

        public string? ManagerContact { get; set; }
    }
}