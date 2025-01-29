
namespace MusicHub.Backend.Models
{
    public class SpotifySettings
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime Expiration { get; set; }
        public virtual required User User { get; set; }

        public SpotifySettings() { }
    }
}