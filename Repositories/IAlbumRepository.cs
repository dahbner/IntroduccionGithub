using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAll();
        Task<Album> GetOne(Guid id);
        Task Add(Album album);
        Task Update(Album album);
        Task Delete(Album album);
    }
}
