using System;
namespace MusicHub.Backend.Models
{
	public class Playlist
	{
        // Unique identifier for the playlist.
        public required int Id { get; set; }

        // Name of the playlist.
        public required string Name { get; set; }

        // Foreign key linking the playlist to a specific user.
        public required int UserId { get; set; }

        // Navigation property to the related user.
        public required User User { get; set; }

        // List of songs included in the playlist.
        public required List<Songs> Songs { get; set; } = new List<Songs>();

    }
}

