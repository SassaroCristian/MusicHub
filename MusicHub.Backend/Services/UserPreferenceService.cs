using System;
using MusicHub.Backend.Data;
using MusicHub.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicHub.Backend.Services
{
    public class UserPreferenceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserPreferenceService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        // Save user preferences
        public async Task SaveUserPreferencesAsync(string genre, string playlistId)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name; // Adjust to match your user identifier

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated.");
            }

            var userPreference = new UserPreference
            {
                UserId = userId,
                Genre = genre,
                PlaylistId = playlistId
            };

            _context.UserPreference.Add(userPreference);
            await _context.SaveChangesAsync();
        }

        // Retrieve user preferences
        public async Task<List<UserPreference>> GetUserPreferencesAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated.");
            }

            return await _context.UserPreference.Where(up => up.UserId == userId).ToListAsync();
        }
    }
}