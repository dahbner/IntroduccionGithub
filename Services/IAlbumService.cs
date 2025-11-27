using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
namespace MiniSpotify.Services
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumResponseDto>> GetAll();
        Task<AlbumResponseDto?> GetOne(Guid id);
        Task<AlbumResponseDto> CreateAlbum(CreateAlbumDto dto);
        Task<AlbumResponseDto> UpdateAlbum(UpdateAlbumDto dto, Guid id);
        Task DeleteAlbum(Guid id);
    }
}
