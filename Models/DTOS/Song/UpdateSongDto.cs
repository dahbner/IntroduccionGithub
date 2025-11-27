namespace MiniSpotify.Models.DTOS
{
    public record UpdateSongDto
    {
        public string Title { get; set; }
        public int DurationSeconds { get; set; }
        public Guid AlbumId { get; set; }
    }
}
