using System.ComponentModel.DataAnnotations;

namespace MiniSpotify.Models.DTOS
{
    public class UpdateAlbumDto
    {
        [MaxLength(150)]
        public string Title { get; set; }  
        public DateTime ReleaseDate { get; set; }
        [Url] public string? CoverUrl { get; set; }
    }
}
