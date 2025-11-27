using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public record ArtistDetailResponseDto
    {
        public string Biography { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? ManagerContact { get; set; }
    }
}