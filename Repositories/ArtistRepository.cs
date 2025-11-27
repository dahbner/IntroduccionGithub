using Microsoft.EntityFrameworkCore;
using MiniSpotify.Data;
using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists
                .Include(a => a.ArtistDetail)
                .Include(a => a.Albums) 
                .ToListAsync();
        }

        public async Task<Artist?> GetByIdAsync(Guid id)
        {
            return await _context.Artists
                .Include(a => a.ArtistDetail)
                .Include(a => a.Albums)
                .ThenInclude(al => al.Songs)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Artist artist)
        {
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }
    }
}