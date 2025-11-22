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
            if(user==null) throw new Exception("User doesnt exist.");
            var playlist = new Playlist
            {
                Id = Guid.NewGuid(),
                IsPublic = dto.IsPublic,
                Name =  dto.Name,
                UserId = userId,
                User = user,
            };
            await _playlistRepo.Add(playlist);
            return playlist;
        }

        public async Task<IEnumerable<Playlist>> GetAll()
        {
            return await _playlistRepo.GetAll();
        }

        public async Task<Playlist> GetOne(Guid id)
        {
            return await _playlistRepo.GetOne(id);
        }
        public async Task<Playlist> UpdatePlaylist(UpdatePlaylistDto dto, Guid id, Guid userId)
        {
            Playlist? playlist = await GetOne(id);
            if (playlist == null) throw new Exception("Playlist doesnt exist.");
            
            if(playlist.UserId != userId) throw new Exception("Not authorized to update this playlist.");
            
            playlist.Name = dto.Name;
            playlist.IsPublic = dto.IsPublic;
            
            await _playlistRepo.Update(playlist);
            return playlist;
        }
        public async Task DeletePlaylist(Guid id,Guid userId)
        {
            Playlist? playlist = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (playlist == null) return;
            if(playlist.UserId != userId) throw new Exception("Not authorized to delete this playlist.");
            
            await _playlistRepo.Delete(playlist);
        }
    }
}
