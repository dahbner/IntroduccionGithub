using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface ISongService
    {
        Task<IEnumerable<SongResponseDto>> GetAll();
        Task<SongResponseDto> GetOne(Guid id);
        Task<SongResponseDto> CreateSong(CreateSongDto dto);
        Task<SongResponseDto> UpdateSong(UpdateSongDto dto, Guid id);
        Task DeleteSong(Guid id);
    }
}
