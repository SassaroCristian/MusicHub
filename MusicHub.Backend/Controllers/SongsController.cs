using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicHub.Backend.Data;
using MusicHub.Backend.Models;

namespace MusicHub.Backend.Controllers
{
    [Route("api/_[controller")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        // Action method to retrieve all songs from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Songs>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/{id}
        // Action method to retrieve a single song by its unique ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Songs>> GetSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }
        // POST: api/Songs
        // Action method to create a new song entry in the database
        [HttpPost]
        public async Task<ActionResult<Songs>> PostSong(Songs song)
        {
            _context.Songs.Add(song);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
        }

        // PUT: api/Songs/{id}
        // Action method to update an existing song in the database
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Songs song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }
            _context.Entry(song).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}