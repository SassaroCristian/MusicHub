using System;
namespace MusicHub.Backend.Models
{
	public class Playlist
	{
        // Unique identifier for the playlist.
        public int Id { get; set; }

        // Name of the playlist.
        public string Name { get; set; }

        // Foreign key linking the playlist to a specific user.
        public int UserId { get; set; }

        // Navigation property to the related user.
        public User User { get; set; }

        // List of songs included in the playlist.
        public List<Songs> Songs { get; set; } = new List<Songs>();
    }
}

