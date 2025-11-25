using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetAll();
        Task<Song> GetOne(Guid id);
        Task<Song> CreateSong(CreateSongDto dto);
        Task<Song> UpdateSong(UpdateSongDto dto, Guid id);
        Task DeleteSong(Guid id);
    }
}
