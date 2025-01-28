using System;
namespace MusicHub.Backend.Models
{
	public class User
	{
        public required int Id { get; set; }
        public required string SpotifyUserId { get; set; }
        public required string Email { get; set; }
        public required DateTime CreatedAt { get; set; }

        // Navigation property for the songs associated with this user
        public required List<Songs> Songs { get; set; }  // Corrected property name to match DbSet

        // Navigation property for SpotifySettings related to this user
        public required SpotifySettings SpotifySettings { get; set; }

        // Navigation property for playlists related to this user
        public required List<Playlist> Playlists { get; set; } = new List<Playlist>();


        public User(string spotifyUserId, string email)
        {
            SpotifyUserId = spotifyUserId;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

