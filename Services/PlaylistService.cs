using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Repositories;

namespace MiniSpotify.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepo;
        private readonly IUserRepository _userRepo;

        public PlaylistService(IPlaylistRepository playlistRepo, IUserRepository userRepo)
        {
            _playlistRepo = playlistRepo;
            _userRepo = userRepo;
        }

        public async Task<Playlist> CreatePlaylist(CreatePlaylistDto dto, Guid userId)
        {
            User? user = await _userRepo.GetById(userId);
            if (user == null) throw new KeyNotFoundException("User not found");

            var playlist = new Playlist
            {
                Id = Guid.NewGuid(),
                IsPublic = dto.IsPublic,
                Name = dto.Name,
                UserId = userId,
            };
            
            await _playlistRepo.Add(playlist);
            return playlist;
        }

        public async Task<IEnumerable<Playlist>> GetAll(Guid userId)
        {
            return await _playlistRepo.GetAll(userId);
        }

        public async Task<Playlist?> GetOne(Guid id)
        {
            return await _playlistRepo.GetOne(id);
        }

        public async Task<Playlist> UpdatePlaylist(UpdatePlaylistDto dto, Guid id, Guid userId)
        {
            Playlist? playlist = await _playlistRepo.GetOne(id);
            if (playlist == null) throw new KeyNotFoundException("Playlist not found");
            
            if (playlist.UserId != userId) 
                throw new UnauthorizedAccessException("You do not own this playlist");

            playlist.Name = dto.Name;
            playlist.IsPublic = dto.IsPublic;

            await _playlistRepo.Update(playlist);
            return playlist;
        }

        public async Task DeletePlaylist(Guid id, Guid userId)
        {
            Playlist? playlist = await _playlistRepo.GetOne(id);
            if (playlist == null) throw new KeyNotFoundException("Playlist not found");
            
            if (playlist.UserId != userId) 
                throw new UnauthorizedAccessException("You do not own this playlist");

            await _playlistRepo.Delete(playlist);
        }
    }
}