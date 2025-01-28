using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static MusicHub.Backend.Models.Songs;

namespace MusicHub.Backend.Models
{
	public class Songs
	{
            public required int Id { get; set; }
            public required string Title { get; set; }
            public required string Artist { get; set; }
            public required string Genre { get; set; }
            public required string SpotifyId { get; set; }
            public required string Album { get; set; }
            public required string ImageUrl { get; set; }

            // Foreign key to link the song to a specific user
            public required int UserId { get; set; }

            // Navigation property to the related user
            public required User User { get; set; }

            // Navigation property for playlists that include this song
            public required List<Playlist> Playlists { get; set; } = new List<Playlist>();
        }
    }


