using Microsoft.EntityFrameworkCore;
using MiniSpotify.Data;
using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _db;

        public AlbumRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Album album)
        {
            await _db.Albums.AddAsync(album);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            return await _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .ToListAsync();
        }

        public async Task<Album?> GetOne(Guid id)
        {
            return await _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Album album)
        {
            _db.Albums.Update(album);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Album album)
        {
            _db.Albums.Remove(album);
            await _db.SaveChangesAsync();
        }
    }
}