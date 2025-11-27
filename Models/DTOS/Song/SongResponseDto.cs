using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public record SongResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public string Album { get; set; }
    }
}
