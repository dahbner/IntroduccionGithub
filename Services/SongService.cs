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
        public async Task<SongResponseDto> CreateSong(CreateSongDto dto)
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
            var songResponse = new SongResponseDto
            {
                Id = song.Id,
                Title = song.Title,
                DurationSeconds = song.DurationSeconds,
                Album = album.Title
            };
            return songResponse;
        }

        public async Task<IEnumerable<SongResponseDto>> GetAll()
        {
            var songs=await _songRepo.GetAll();
            var songsResponseDtos = songs.Select(song => new SongResponseDto
            {
                Id = song.Id,
                Title = song.Title,
                DurationSeconds = song.DurationSeconds,
                Album = song.Album.Title
            });
            return songsResponseDtos;
        }

        public async Task<SongResponseDto?> GetOne(Guid id)
        {
            Song? song =  await _songRepo.GetOne(id);
            if (song == null) throw new Exception("Song doesnt exist.");
            var songResponse = new SongResponseDto
            {
                Id = song.Id,
                Title = song.Title,
                DurationSeconds = song.DurationSeconds,
                Album = song.Album.Title
            };
            return songResponse;
        }
        public async Task<SongResponseDto> UpdateSong(UpdateSongDto dto, Guid id)
        {
            Song? song = await _songRepo.GetOne(id);
            if (song == null) throw new Exception("Song doesnt exist.");
            Album? album = await _albumRepo.GetOne(dto.AlbumId);
            if (album == null) throw new Exception("Album doesnt exist.");

            song.Title = dto.Title;
            song.DurationSeconds = dto.DurationSeconds;
            song.AlbumId = dto.AlbumId;
            song.Album = album;
            
            await _songRepo.Update(song);
            
            var songResponse = new SongResponseDto
            {
                Id = song.Id,
                Title = song.Title,
                DurationSeconds = song.DurationSeconds,
                Album = album.Title
            };
            
            return songResponse;
        }
        public async Task DeleteSong(Guid id)
        {
            Song? song = await _songRepo.GetOne(id);
            if (song == null) return;
            await _songRepo.Delete(song);
        }
    }
}
