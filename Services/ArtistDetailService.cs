using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class ArtistDetailService : IArtistDetailService
    {
        private readonly IArtistDetailRepository _repository;
        private readonly IArtistRepository _artistRepository;

        public ArtistDetailService(IArtistDetailRepository repository, IArtistRepository artistRepository)
        {
            _repository = repository;
            _artistRepository = artistRepository;
        }

        public async Task<ArtistDetailResponseDto?> GetByArtistIdAsync(Guid artistId)
        {
            var detail= await _repository.GetByArtistIdAsync(artistId);
            return new ArtistDetailResponseDto
            {
                Id = detail.Id,
                Biography = detail.Biography,
                WebsiteUrl = detail.WebsiteUrl,
                ManagerContact = detail.ManagerContact,
            };
        }

        public async Task<ArtistDetailResponseDto> CreateAsync(CreateArtistDetailDto dto)
        {
            var artist = await _artistRepository.GetByIdAsync(dto.ArtistId);
            if (artist == null) throw new Exception("Artist not found");

            var detail = new ArtistDetail
            {
                Id = Guid.NewGuid(),
                Biography = dto.Biography,
                WebsiteUrl = dto.WebsiteUrl,
                ManagerContact = dto.ManagerContact,
                ArtistId = dto.ArtistId
            };

            await _repository.AddAsync(detail);
            return new ArtistDetailResponseDto
            {
                Id = detail.Id,
                Biography = detail.Biography,
                WebsiteUrl = detail.WebsiteUrl,
                ManagerContact = detail.ManagerContact,
            };
        }

        public async Task<bool> UpdateAsync(Guid artistId, UpdateArtistDetailDto dto)
        {
            var existingDetail = await _repository.GetByArtistIdAsync(artistId);
            if (existingDetail == null) return false;
            
            existingDetail.Biography = dto.Biography;
            existingDetail.WebsiteUrl = dto.WebsiteUrl;
            existingDetail.ManagerContact = dto.ManagerContact;

            await _repository.UpdateAsync(existingDetail);
            return true;
        }
    }
}