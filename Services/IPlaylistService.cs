using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface IPlaylistService
    {
        Task<IEnumerable<PlaylistResponseDto>> GetAll(Guid userId);
        Task<PlaylistResponseDto> GetOne(Guid id);
        Task<PlaylistResponseDto> CreatePlaylist(CreatePlaylistDto dto, Guid userId);
        Task<PlaylistResponseDto> UpdatePlaylist(UpdatePlaylistDto dto, Guid id, Guid userId);
        Task DeletePlaylist(Guid id, Guid userId);
        Task AddSongToPlaylist(Guid playlistId, Guid songId, Guid userId);
        Task RemoveSongFromPlaylist(Guid playlistId, Guid songId, Guid userId);
    }
}
