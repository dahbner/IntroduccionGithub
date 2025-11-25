using Microsoft.EntityFrameworkCore;
using MiniSpotify.Data;
using MiniSpotify.Models;

namespace MiniSpotify.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly AppDbContext _db;
        public PlaylistRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Playlist playlist)
        {
            await _db.Playlists.AddAsync(playlist);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Playlist>> GetAll()
        {
            return await _db.Playlists.ToListAsync();
        }

        public async Task<Playlist?> GetOne(Guid id)
        {
            return await _db.Playlists.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Update(Playlist playlist)
        {
            _db.Playlists.Update(playlist);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Playlist playlist)
        {
            _db.Playlists.Remove(playlist);
            await _db.SaveChangesAsync();
        }

    }
}
