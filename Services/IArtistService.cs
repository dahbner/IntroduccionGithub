using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(Guid id);
        Task<Artist> CreateAsync(CreateArtistDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateArtistDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}