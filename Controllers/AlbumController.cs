using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Services;

namespace MiniSpotify.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;
        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            IEnumerable<Album> items = await _service.GetAll();
            return Ok(items);
        }


        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var album = await _service.GetOne(id);
            return Ok(album);
        }


        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumDto dto,Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var album = await _service.CreateAlbum(dto,id);
            return CreatedAtAction(nameof(GetOne), new { id = album.Id }, album);
        }



        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateAlbum([FromBody] UpdateAlbumDto dto, Guid id,Guid albumId)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var album = await _service.UpdateAlbum(dto, id,albumId);
            return CreatedAtAction(nameof(GetOne), new { id = album.Id }, album);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAlbum(Guid id,Guid albumId)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteAlbum(id,albumId);
            return NoContent();
        }
    }
}
