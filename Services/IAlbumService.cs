using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
namespace MiniSpotify.Services
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAll();
        Task<Album> GetOne(Guid id);
        Task<Album> CreateAlbum(CreateAlbumDto dto, Guid artistId);
        Task<Album> UpdateAlbum(UpdateAlbumDto dto, Guid id,  Guid albumId);
        Task DeleteAlbum(Guid id, Guid userId);
        

    }
}
