using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _repository;

        public ArtistService(IArtistRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ArtistResponseDto>> GetAllAsync() //Get All Artists
        {
            var artists = await _repository.GetAllAsync();
            return artists.Select(a => new ArtistResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Genre = a.Genre,

                ArtistDetail = a.ArtistDetail != null ? new ArtistDetailResponseDto
                {
                    Id = a.ArtistDetail.Id,
                    Biography = a.ArtistDetail.Biography,
                    ManagerContact = a.ArtistDetail.ManagerContact,
                    WebsiteUrl = a.ArtistDetail.WebsiteUrl,
                } : null,
                Albums = a.Albums.Select(al => new AlbumResponseDto
                {
                    Id = al.Id,
                    Artist = al.Artist.Name,
                    Title = al.Title,
                    CoverUrl = al.CoverUrl,
                    ReleaseDate = al.ReleaseDate,
                    Songs = al.Songs.Select(s => new SongResponseDto
                    {
                        Id = s.Id,
                        Album = s.Album.Title,
                        Title = s.Title,
                        DurationSeconds = s.DurationSeconds
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public async Task<ArtistResponseDto?> GetByIdAsync(Guid id)
        {
            var artist = await _repository.GetByIdAsync(id);
            if (artist == null) return null; 

            return new ArtistResponseDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
               
                ArtistDetail = artist.ArtistDetail != null ? new ArtistDetailResponseDto
                {
                    Id = artist.ArtistDetail.Id,
                    Biography = artist.ArtistDetail.Biography,
                    ManagerContact = artist.ArtistDetail.ManagerContact,
                    WebsiteUrl = artist.ArtistDetail.WebsiteUrl,
                } : null,
                Albums = artist.Albums.Select(al => new AlbumResponseDto
                {
                    Id = al.Id,
                    Artist = al.Artist.Name,
                    Title = al.Title,
                    CoverUrl = al.CoverUrl,
                    ReleaseDate = al.ReleaseDate,
                    Songs = al.Songs.Select(s => new SongResponseDto
                    {
                        Id = s.Id,
                        Album = s.Album.Title,
                        Title = s.Title,
                        DurationSeconds = s.DurationSeconds
                    }).ToList()
                }).ToList()
            };
        }

      
        public async Task<ArtistResponseDto> CreateAsync(CreateArtistDto dto)
        {
            var artist = new Artist
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Genre = dto.Genre
               
            };

            await _repository.AddAsync(artist);

            return new ArtistResponseDto
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
               
                ArtistDetail = null,

                Albums = new List<AlbumResponseDto>()
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateArtistDto dto)
        {
            var existingArtist = await _repository.GetByIdAsync(id);
            if (existingArtist == null) return false;

            existingArtist.Name = dto.Name;
            existingArtist.Genre = dto.Genre;

            await _repository.UpdateAsync(existingArtist);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingArtist = await _repository.GetByIdAsync(id);
            if (existingArtist == null) return false;

            await _repository.DeleteAsync(existingArtist);
            return true;
        }
    }
}