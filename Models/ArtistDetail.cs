using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSpotify.Models
{
    public class ArtistDetail
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Biography { get; set; }

        [Url]
        public string? WebsiteUrl { get; set; }

        public string? ManagerContact { get; set; }

        [Required]
        public Guid ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        [JsonIgnore]
        public Artist Artist { get; set; }
    }
}