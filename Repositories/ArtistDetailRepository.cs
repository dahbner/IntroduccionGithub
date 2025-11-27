using Microsoft.EntityFrameworkCore;
using MiniSpotify.Data;
using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public class ArtistDetailRepository : IArtistDetailRepository
    {
        private readonly AppDbContext _context;

        public ArtistDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ArtistDetail?> GetByArtistIdAsync(Guid artistId)
        {
            return await _context.ArtistDetails
                .FirstOrDefaultAsync(ad => ad.ArtistId == artistId);
        }

        public async Task AddAsync(ArtistDetail artistDetail)
        {
            await _context.ArtistDetails.AddAsync(artistDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ArtistDetail artistDetail)
        {
            _context.ArtistDetails.Update(artistDetail);
            await _context.SaveChangesAsync();
        }
    }
}