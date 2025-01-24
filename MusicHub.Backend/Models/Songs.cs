using System;
namespace MusicHub.Backend.Models
{
	public class Songs
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }  
        public string ImageUrl { get; set; }  
        public string PreviewUrl { get; set; }
    }
}

