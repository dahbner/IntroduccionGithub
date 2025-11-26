using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepo;
        // private readonly IArtistRepository _artistRepo; 

        public AlbumService(IAlbumRepository albumRepo /*, IArtistRepository artistRepo */)
        {
            _albumRepo = albumRepo;
            // _artistRepo = artistRepo;
        }

        public async Task<Album> CreateAlbum(CreateAlbumDto dto)
        {
            // var artist = await _artistRepo.GetOne(dto.ArtistId);
            // if (artist == null) throw new KeyNotFoundException("Artist not found");

            var album = new Album
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate,
                CoverUrl = dto.CoverUrl,
                ArtistId = dto.ArtistId
            };

            await _albumRepo.Add(album);
            return album;
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            return await _albumRepo.GetAll();
        }

        public async Task<Album?> GetOne(Guid id)
        {
            return await _albumRepo.GetOne(id);
        }

        public async Task<Album> UpdateAlbum(UpdateAlbumDto dto, Guid id)
        {
            Album? album = await _albumRepo.GetOne(id);
            if (album == null) throw new KeyNotFoundException("Album not found");
            
            album.Title = dto.Title;
            album.ReleaseDate = dto.ReleaseDate;
            if(dto.CoverUrl != null) album.CoverUrl = dto.CoverUrl;

            await _albumRepo.Update(album);
            return album;
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