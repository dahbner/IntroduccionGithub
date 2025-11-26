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

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Artist?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Artist> CreateAsync(CreateArtistDto dto)
        {
            var artist = new Artist
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Genre = dto.Genre
            };

            await _repository.AddAsync(artist);
            return artist;
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