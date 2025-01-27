using System;
namespace MusicHub.Backend.Models
{
	public class User
	{
        public int Id { get; set; }
        public string SpotifyUserId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property for the songs associated with this user
        public List<Songs> Songs { get; set; }  // Corrected property name to match DbSet

        // Navigation property for SpotifySettings related to this user
        public SpotifySettings SpotifySettings { get; set; }

        // Navigation property for playlists related to this user
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}

