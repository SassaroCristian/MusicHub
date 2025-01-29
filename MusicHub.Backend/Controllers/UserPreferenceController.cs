using System;
using Microsoft.AspNetCore.Mvc;
using MusicHub.Backend.Services;

namespace MusicHub.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPreferencesController : ControllerBase
    {
        private readonly UserPreferenceService _userPreferenceService;

        public UserPreferencesController(UserPreferenceService userPreferenceService)
        {
            _userPreferenceService = userPreferenceService;
        }

        // POST: api/userpreferences
        [HttpPost]
        public async Task<IActionResult> SavePreferences([FromBody] UserPreferenceDto preference)
        {
            if (preference == null)
            {
                return BadRequest("Invalid preference data.");
            }

            await _userPreferenceService.SaveUserPreferencesAsync(preference.Genre, preference.PlaylistId);
            return Ok("Preferences saved successfully.");
        }

        // GET: api/userpreferences
        [HttpGet]
        public async Task<IActionResult> GetPreferences()
        {
            var preferences = await _userPreferenceService.GetUserPreferencesAsync();
            return Ok(preferences);
        }
    }

    public class UserPreferenceDto
    {
        public string Genre { get; set; }
        public string PlaylistId { get; set; }
    }
}
