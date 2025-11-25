using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepo;
        private readonly IAlbumRepository _albumRepo;
        public SongService(ISongRepository songRepo, IAlbumRepository albumRepo)
        {
            _songRepo = songRepo;
            _albumRepo = albumRepo;
        }
        public async Task<Song> CreateSong(CreateSongDto dto)
        {
            Album album = await _albumRepo.GetOne(dto.AlbumId);
            if (album == null) throw new Exception("Album doesnt exist.");
            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title =  dto.Title,
                DurationSeconds = dto.DurationSeconds,
                AlbumId =  dto.AlbumId,
                Album = album
            };
            await _songRepo.Add(song);
            return song;
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            return await _songRepo.GetAll();
        }

        public async Task<Song> GetOne(Guid id)
        {
            return await _songRepo.GetOne(id);
        }
        public async Task<Song> UpdateSong(UpdateSongDto dto, Guid id)
        {
            Song? song = await GetOne(id);
            if (song == null) throw new Exception("Song doesnt exist.");
            Album? album = await _albumRepo.GetOne(dto.AlbumId);
            if (album == null) throw new Exception("Album doesnt exist.");

            song.Title = dto.Title;
            song.DurationSeconds = dto.DurationSeconds;
            song.AlbumId = dto.AlbumId;
            song.Album = album;
            
            await _songRepo.Update(song);
            return song;
        }
        public async Task DeleteSong(Guid id)
        {
            Song? song = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (song == null) return;
            await _songRepo.Delete(song);
        }
    }
}
