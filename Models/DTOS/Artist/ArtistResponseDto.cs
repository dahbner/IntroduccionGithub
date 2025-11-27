using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Models
{
    public class ArtistResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public ArtistDetailResponseDto? ArtistDetail { get; set; }
        public ICollection<AlbumResponseDto> Albums { get; set; } = new List<AlbumResponseDto>();
    }
}