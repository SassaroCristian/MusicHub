
namespace MusicHub.Backend.Models
{
    public class SpotifySettings
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime Expiration { get; set; }
        public required User User { get; set; }

        // Constructor
        public SpotifySettings(string accessToken, string refreshToken, User user)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
            Expiration = DateTime.UtcNow.AddMinutes(60); // Example: set expiration 1 hour from now
        }
    }
}