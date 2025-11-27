using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistResponseDto>> GetAllAsync();
        Task<ArtistResponseDto?> GetByIdAsync(Guid id);
        Task<ArtistResponseDto> CreateAsync(CreateArtistDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateArtistDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}