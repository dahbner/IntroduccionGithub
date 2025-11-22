using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSpotify.Models
{
    public class Album
    {
        [Key] public Guid Id { get; set; }
        [Required] [MaxLength(150)] public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Url] public string? CoverUrl { get; set; }

        [Required] public Guid ArtistId { get; set; }
        [ForeignKey("ArtistId")] public Artist Artist { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}