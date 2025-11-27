using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface IArtistDetailService
    {
        Task<ArtistDetail?> GetByArtistIdAsync(Guid artistId);
        Task<ArtistDetail> CreateAsync(CreateArtistDetailDto dto);
        Task<bool> UpdateAsync(Guid artistId, UpdateArtistDetailDto dto);
    }
}