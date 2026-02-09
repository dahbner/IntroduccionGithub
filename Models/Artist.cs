using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniSpotify.Models
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")] 
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; }
        public ArtistDetail? ArtistDetail { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}