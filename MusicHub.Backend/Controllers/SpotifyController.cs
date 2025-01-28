using System;
using Microsoft.AspNetCore.Mvc;
using MusicHub.Backend.Services;
using System.Threading.Tasks;

namespace MusicHub.Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpotifyController : ControllerBase
	{
		private readonly SpotifyService _spotifyService;
		public SpotifyController(SpotifyService spotifyService)
		{
			_spotifyService = spotifyService;
		}
		//get songs by genre
		[HttpGet("songs/genre/{genre}")]
		public async Task<IActionResult> GetSongsByGenre(string genre)
		{
			var songs = await _spotifyService.GetSongsByGenreAsync(genre);
			return Ok(songs);
		}
		[HttpGet("playlists")]
		public async Task<IActionResult> GetUserPlaylists()
		{
            var playlists = await _spotifyService.GetUserPlaylistsAsync();
            return Ok(playlists);
        }
	}
}

