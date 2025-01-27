using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static MusicHub.Backend.Models.Songs;

namespace MusicHub.Backend.Models
{
	public class Songs
	{
            public int Id { get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Genre { get; set; }
            public string SpotifyId { get; set; }
            public string Album { get; set; }
            public string ImageUrl { get; set; }

            // Foreign key to link the song to a specific user
            public int UserId { get; set; }

            // Navigation property to the related user
            public User User { get; set; }

            // Navigation property for playlists that include this song
            public List<Playlist> Playlists { get; set; } = new List<Playlist>();
        }
    }


