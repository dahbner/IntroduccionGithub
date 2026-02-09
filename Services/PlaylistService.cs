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

        public async Task<PlaylistResponseDto> CreatePlaylist(CreatePlaylistDto dto, Guid userId) //Este metodo se usa para crear la playlist
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
            
            return new PlaylistResponseDto
            {
                Id= playlist.Id,
                IsPublic =  playlist.IsPublic,
                Name = playlist.Name,
                User = new UserResponse
                {
                    Id = playlist.UserId.ToString(),
                    Email =  playlist.User.Email,
                    Username =  playlist.User.Username,
                },
                Songs = playlist.Songs.Select(s=>new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            };
        }

        public async Task<IEnumerable<PlaylistResponseDto>> GetAll(Guid userId)
        {
            var playlists= await _playlistRepo.GetAll(userId);
            return playlists.Select(playlist => new PlaylistResponseDto
            {
                Id = playlist.Id,
                IsPublic = playlist.IsPublic,
                Name = playlist.Name,
                User = new UserResponse
                {
                    Id = playlist.UserId.ToString(),
                    Email = playlist.User.Email,
                    Username = playlist.User.Username,
                },
                Songs = playlist.Songs.Select(s => new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            });
        }

        public async Task<PlaylistResponseDto?> GetOne(Guid id)
        {
            var playlist= await _playlistRepo.GetOne(id);
            return new PlaylistResponseDto
            {
                Id= playlist.Id,
                IsPublic =  playlist.IsPublic,
                Name = playlist.Name,
                User = new UserResponse
                {
                    Id = playlist.UserId.ToString(),
                    Email =  playlist.User.Email,
                    Username =  playlist.User.Username,
                },
                Songs = playlist.Songs.Select(s=>new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            };
        }

        public async Task<PlaylistResponseDto> UpdatePlaylist(UpdatePlaylistDto dto, Guid id, Guid userId)
        {
            Playlist? playlist = await _playlistRepo.GetOne(id);
            if (playlist == null) throw new KeyNotFoundException("Playlist not found");
            
            if (playlist.UserId != userId) 
                throw new UnauthorizedAccessException("You do not own this playlist");

            playlist.Name = dto.Name;
            playlist.IsPublic = dto.IsPublic;

            await _playlistRepo.Update(playlist);
            return new PlaylistResponseDto
            {
                Id= playlist.Id,
                IsPublic =  playlist.IsPublic,
                Name = playlist.Name,
                User = new UserResponse
                {
                    Id = playlist.UserId.ToString(),
                    Email =  playlist.User.Email,
                    Username =  playlist.User.Username,
                },
                Songs = playlist.Songs.Select(s=>new SongResponseDto
                {
                    Id = s.Id,
                    Album = s.Album.Title,
                    DurationSeconds = s.DurationSeconds,
                    Title = s.Title
                }).ToList()
            };;
        }

        public async Task DeletePlaylist(Guid id, Guid userId)
        {
            Playlist? playlist = await _playlistRepo.GetOne(id);
            if (playlist == null) throw new KeyNotFoundException("Playlist not found");
            
            if (playlist.UserId != userId) 
                throw new UnauthorizedAccessException("You do not own this playlist");

            await _playlistRepo.Delete(playlist);
        }
        public async Task AddSongToPlaylist(Guid playlistId, Guid songId, Guid userId)
        {
            var playlist = await _playlistRepo.GetOne(playlistId);
            if (playlist == null) throw new KeyNotFoundException("Playlist not found");
    
            if (playlist.UserId != userId) 
                throw new UnauthorizedAccessException("You do not own this playlist");
            
            await _playlistRepo.AddSong(playlistId, songId);
        }

        public async Task RemoveSongFromPlaylist(Guid playlistId, Guid songId, Guid userId)
        {
            var playlist = await _playlistRepo.GetOne(playlistId);
            if (playlist == null) throw new KeyNotFoundException("Playlist not found");
    
            if (playlist.UserId != userId) 
                throw new UnauthorizedAccessException("You do not own this playlist");
            
            await _playlistRepo.RemoveSong(playlistId, songId);
        }
    }
}