using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface IPlaylistService
    {
        Task<IEnumerable<Playlist>> GetAll(Guid userId);
        Task<Playlist> GetOne(Guid id);
        Task<Playlist> CreatePlaylist(CreatePlaylistDto dto, Guid userId);
        Task<Playlist> UpdatePlaylist(UpdatePlaylistDto dto, Guid id, Guid userId);
        Task DeletePlaylist(Guid id, Guid userId);
    }
}
