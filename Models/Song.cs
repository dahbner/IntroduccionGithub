using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSpotify.Models
{
    public class Song
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        public int DurationSeconds { get; set; }
        
        [Required]
        public Guid AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        [JsonIgnore]
        public Album Album { get; set; }
        
        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}