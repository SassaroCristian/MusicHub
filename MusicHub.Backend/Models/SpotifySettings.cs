
namespace MusicHub.Backend.Models
{
    public class SpotifySettings
    {
        // Unique identifier for the settings entry.
        public int Id { get; set; }

        // Foreign key linking the settings to a specific user.
        public int UserId { get; set; }

        // Spotify Access Token (used for accessing Spotify API).
        public string AccessToken { get; set; }

        // Spotify Refresh Token (used to refresh the Access Token).
        public string RefreshToken { get; set; }

        // Expiration date of the access token.
        public DateTime Expiration { get; set; }

        // Navigation property to the related user.
        public User User { get; set; }
    }
}