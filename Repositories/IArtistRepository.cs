using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(Guid id);
        Task AddAsync(Artist artist);
        Task UpdateAsync(Artist artist);
        Task DeleteAsync(Artist artist);
    }
}