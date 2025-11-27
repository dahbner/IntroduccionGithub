using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSpotify.Models.DTOS
{
    public record CreatePlaylistDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
