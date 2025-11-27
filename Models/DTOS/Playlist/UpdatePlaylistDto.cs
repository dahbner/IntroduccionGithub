namespace MiniSpotify.Models.DTOS
{
    public record UpdatePlaylistDto
    {
        public string Name { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
