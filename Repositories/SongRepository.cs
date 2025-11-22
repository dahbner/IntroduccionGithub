using Microsoft.EntityFrameworkCore;
using MiniSpotify.Data;
using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _db;
        public SongRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Song book)
        {
            await _db.Songs.AddAsync(book);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            return await _db.Songs.ToListAsync();
        }

        public async Task<Song?> GetOne(Guid id)
        {
            return await _db.Songs.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(Song book)
        {
            _db.Songs.Update(book);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Song book)
        {
            _db.Songs.Remove(book);
            await _db.SaveChangesAsync();
        }

    }
}
