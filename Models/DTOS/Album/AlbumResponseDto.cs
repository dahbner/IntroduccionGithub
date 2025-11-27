using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public class AlbumResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? CoverUrl { get; set; }
        public string Artist { get; set; }
        public ICollection<SongResponseDto> Songs { get; set; } = new List<SongResponseDto>();
    }
}
