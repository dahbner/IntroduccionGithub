using MiniSpotify.Models.DTOS;
using MiniSpotify.Models.DTOS.MiniSpotify.Models.DTOS;

namespace MiniSpotify.Services
{
    public interface IAuthService
    {
        Task<(bool ok, LoginResponseDto? response)> LoginAsync(LoginDto dto);
        Task<bool> LogoutAsync(LogoutDto dto);
        Task<string> RegisterAsync(RegisterDto dto);
        Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto);
    }
}