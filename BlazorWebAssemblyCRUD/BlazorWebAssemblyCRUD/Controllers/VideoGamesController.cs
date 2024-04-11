using BlazorWebAssemblyCRUD.Data;
using BlazorWebAssemblyCRUD.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAssemblyCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly DataContext _context;

        public VideoGamesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAllVideoGamesAsync()
        {
            return await _context.VideoGames.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameByIdAsync(int id)
        {
            var result = await _context.VideoGames.FindAsync(id);

            if (result == null)
            {
                return NotFound("Game not found");
            }
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<VideoGame>> DeleteVideoGameAsync(int id)
        {
            var result = await _context.VideoGames.FindAsync(id);

            if (result == null)
            {
                return NotFound("Game not found");
            }

            _context.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }   
        
        [HttpPut("{id}")]
        public async Task<ActionResult<VideoGame>> UpdateVideoGameAsync(int id, VideoGame updatedGame)
        {
            var dbGame = await _context.VideoGames.FindAsync(id);

            if (dbGame == null)
            {
                return NotFound("Game not found");
            }

            dbGame.Title = updatedGame.Title;
            dbGame.Publisher = updatedGame.Publisher;
            dbGame.ReleaseYear = updatedGame.ReleaseYear;

            await _context.SaveChangesAsync();

            return Ok(dbGame);
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddVideoGameAsync(VideoGame newGame)
        {
            _context.Add(newGame);

            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
