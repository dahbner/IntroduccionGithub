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

        public async Task<IEnumerable<Playlist>> GetAll(Guid userId)
        {
            return await _db.Playlists
                .Where(p => p.IsPublic == true || p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.Songs)
                .ThenInclude(s => s.Album)
                .ToListAsync();
        }

        public async Task<Playlist?> GetOne(Guid id)
        {
            return await _db.Playlists
                .Include(p => p.User)
                .Include(p => p.Songs)
                .ThenInclude(s => s.Album)
                .FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task AddSong(Guid playlistId, Guid songId)
        {
            var playlist = await _db.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.Id == playlistId);

            var song = await _db.Songs.FindAsync(songId);

            if (playlist != null && song != null)
            {
                if (!playlist.Songs.Any(s => s.Id == songId))
                {
                    playlist.Songs.Add(song);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveSong(Guid playlistId, Guid songId)
        {
            var playlist = await _db.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(p => p.Id == playlistId);
            
            var song = playlist?.Songs.FirstOrDefault(s => s.Id == songId);

            if (playlist != null && song != null)
            {
                playlist.Songs.Remove(song);
                await _db.SaveChangesAsync();
            }
        }
    }
}