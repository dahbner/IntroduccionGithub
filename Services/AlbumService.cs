using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepo;
        public AlbumService(IAlbumRepository albumRepo)
        {
            _albumRepo = albumRepo;
        }


        public async Task<Album> CreateAlbum(CreateAlbumDto dto, Guid albumId)
        {
            Album? album = await _albumRepo.GetOne(albumId);
            if (album == null) throw new Exception("The album doesnt exist:");
            var Album = new Album
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate

            };
            await _albumRepo.Add(album);
            return album;
        }

        public async Task DeleteAlbum(Guid id, Guid albumId)
        {
            Album? album = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (album == null) return;
            if (album.Id != albumId) throw new Exception("Not authorized to delete this album:");

            await _albumRepo.Delete(album);
        }

        public async Task<IEnumerable<Album>> GetAll()
        {
            return await _albumRepo.GetAll();
        }

        public async Task<Album> GetOne(Guid id)
        {
            return await _albumRepo.GetOne(id);
        }

        public async Task<Album> UpdateAlbum(UpdateAlbumDto dto, Guid id, Guid albumId)
        {
            Album? album = await GetOne(id);
            if (album == null) throw new Exception("The album you're looking for doesnt exist.");

            if (album.Id != albumId) throw new Exception("Not authorized to update this album.");

            album.Title = dto.Title;

            await _albumRepo.Update(album);
            return album;
        }
    }
}
