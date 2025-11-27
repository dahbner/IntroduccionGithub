using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSpotify.Models;
using MiniSpotify.Models.DTOS;
using MiniSpotify.Services;

namespace MiniSpotify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistDetailController : ControllerBase
    {
        private readonly IArtistDetailService _service;

        public ArtistDetailController(IArtistDetailService service)
        {
            _service = service;
        }

        [HttpGet("{artistId}")]
        public async Task<IActionResult> GetByArtistId(Guid artistId)
        {
            var result = await _service.GetByArtistIdAsync(artistId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(CreateArtistDetailDto dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByArtistId), new { artistId = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{artistId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Guid artistId, UpdateArtistDetailDto dto)
        {
            var result = await _service.UpdateAsync(artistId, dto);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}