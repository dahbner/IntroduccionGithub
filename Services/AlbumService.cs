using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepo;
        private readonly IArtistRepository _artistRepo; 

        public AlbumService(IAlbumRepository albumRepo, IArtistRepository artistRepo)
        {
            _albumRepo = albumRepo;
            _artistRepo = artistRepo;
        }

        public async Task<AlbumResponseDto> CreateAlbum(CreateAlbumDto dto)
        {
            var artist = await _artistRepo.GetByIdAsync(dto.ArtistId);
            if (artist == null) throw new KeyNotFoundException("Artist not found");

            var album = new Album
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate,
                CoverUrl = dto.CoverUrl,
                ArtistId = dto.ArtistId
            };

            await _albumRepo.Add(album);

            var albumResponse = new AlbumResponseDto
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverUrl = album.CoverUrl,
                Artist = artist.Name,
                Songs = new List<SongResponseDto>()
            };
            return albumResponse;
        }

        public async Task<IEnumerable<AlbumResponseDto>> GetAll()
        {
            var albums = await _albumRepo.GetAll();
            return albums.Select(a => new AlbumResponseDto
            {
                Id = a.Id,
                Title = a.Title,
                ReleaseDate = a.ReleaseDate,
                CoverUrl = a.CoverUrl,
                Artist = a.Artist.Name,
                Songs = a.Songs.Select(s => new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            });
        }

        public async Task<AlbumResponseDto?> GetOne(Guid id)
        {
            var album= await _albumRepo.GetOne(id);
            return new AlbumResponseDto
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverUrl = album.CoverUrl,
                Artist = album.Artist.Name,
                Songs = album.Songs.Select(s => new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            };
        }

        public async Task<AlbumResponseDto> UpdateAlbum(UpdateAlbumDto dto, Guid id)
        {
            Album? album = await _albumRepo.GetOne(id);
            if (album == null) throw new KeyNotFoundException("Album not found");
            
            album.Title = dto.Title;
            album.ReleaseDate = dto.ReleaseDate;
            if(dto.CoverUrl != null) album.CoverUrl = dto.CoverUrl;

            await _albumRepo.Update(album);
            
            return new AlbumResponseDto
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverUrl = album.CoverUrl,
                Artist = album.Artist.Name,
                Songs = album.Songs.Select(s => new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            };
        }

        public async Task DeleteAlbum(Guid id)
        {
            Album? album = await _albumRepo.GetOne(id);
            if (album != null)
            {
                await _albumRepo.Delete(album);
            }
        }
    }
}