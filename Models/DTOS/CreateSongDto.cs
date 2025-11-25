using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public record CreateSongDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int DurationSeconds { get; set; }
        [Required]
        public Guid AlbumId { get; set; }
    }
}
