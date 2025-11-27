using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public interface IArtistDetailRepository
    {
        Task<ArtistDetail?> GetByArtistIdAsync(Guid artistId);
        Task AddAsync(ArtistDetail artistDetail);
        Task UpdateAsync(ArtistDetail artistDetail);
    }
}