using System;
namespace MusicHub.Backend.Models
{
	public class UserPreference
	{
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to the user (could be the Spotify user ID or your app's user ID)
        public string Genre { get; set; } // Can store a single genre or a comma-separated list if multiple genres
        public string PlaylistId { get; set; } // Store the playlist ID if relevant

    }
}

