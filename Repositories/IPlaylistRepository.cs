using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Playlist>> GetAll();
        Task<Playlist> GetOne(Guid id);
        Task Add(Playlist playlist);
        Task Update(Playlist playlist);
        Task Delete(Playlist playlist);
    }
}
