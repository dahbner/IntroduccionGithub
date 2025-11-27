using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Models
{
    public class PlaylistResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; } = false;
        public UserResponse User { get; set; }
        
        public ICollection<SongResponseDto> Songs { get; set; } = new List<SongResponseDto>();
    }
}