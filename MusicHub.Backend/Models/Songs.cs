using System;
namespace MusicHub.Backend.Models
{
	public class Songs
	{
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Artist { get; set; }
        public required string Genre { get; set; }
        public required string SpotifyId { get; set; }  
        public required string ImageUrl { get; set; }  
        public required string PreviewUrl { get; set; }
    }
}

