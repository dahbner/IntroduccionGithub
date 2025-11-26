using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Services;
using System.IdentityModel.Tokens.Jwt;

namespace MiniSpotify.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _service;

        public PlaylistController(IPlaylistService service)
        {
            _service = service;
        }

        private Guid GetCurrentUserId()
        {
            var idClaim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? User.FindFirst(ClaimTypes.NameIdentifier);
            
            if (idClaim == null) throw new UnauthorizedAccessException("Invalid Token: No ID found");
            
            return Guid.Parse(idClaim.Value);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var userId = GetCurrentUserId();
            IEnumerable<Playlist> items = await _service.GetAll(userId);
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var playlist = await _service.GetOne(id);
            if (playlist == null) return NotFound();
            return Ok(playlist);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlaylist([FromBody] CreatePlaylistDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            
            var userId = GetCurrentUserId();
            var playlist = await _service.CreatePlaylist(dto, userId);
            
            return CreatedAtAction(nameof(GetOne), new { id = playlist.Id }, playlist);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdatePlaylist([FromBody] UpdatePlaylistDto dto, Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            
            var userId = GetCurrentUserId();
            
            try 
            {
                var playlist = await _service.UpdatePlaylist(dto, id, userId);
                return Ok(playlist);
            }
            catch (UnauthorizedAccessException) { return Forbid(); }
            catch (KeyNotFoundException) { return NotFound(); }
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeletePlaylist(Guid id)
        {
            var userId = GetCurrentUserId();
            
            try
            {
                await _service.DeletePlaylist(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException) { return Forbid(); }
            catch (KeyNotFoundException) { return NotFound(); }
        }
        [HttpPost("{id:guid}/songs/{songId:guid}")]
        [Authorize]
        public async Task<IActionResult> AddSongToPlaylist(Guid id, Guid songId)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _service.AddSongToPlaylist(id, songId, userId);
                return Ok(new { message = "Song added to playlist" });
            }
            catch (UnauthorizedAccessException) { return Forbid(); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        [HttpDelete("{id:guid}/songs/{songId:guid}")]
        [Authorize]
        public async Task<IActionResult> RemoveSongFromPlaylist(Guid id, Guid songId)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _service.RemoveSongFromPlaylist(id, songId, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException) { return Forbid(); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}