using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MusicHub.Backend.Services
{
    public class SpotifyService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SpotifyService> _logger;

        public SpotifyService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, ILogger<SpotifyService> logger)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(HttpContextAccessor));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");

            if (accessToken == null)
            {
                // Handle the case where the access token is missing, maybe throw an exception or return a default value
                throw new InvalidOperationException("Access token is missing.");
            }

            // Check if the token is expired, and if so, refresh it
            var expirationTime = GetTokenExpirationTime();
            if (DateTime.UtcNow > expirationTime)
            {
                accessToken = await RefreshAccessTokenAsync();
            }

            return accessToken;
        }

        private async Task<string> RefreshAccessTokenAsync()
        {
            var refreshToken = GetRefreshToken();

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Refresh token is missing.");
            }

            // Send a request to Spotify's API to get a new access token
            var requestContent = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            new KeyValuePair<string, string>("client_id", "<your_client_id>"),
            new KeyValuePair<string, string>("client_secret", "<your_client_secret>")
        });

            var response = await _httpClient.PostAsync("https://accounts.spotify.com/api/token", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                // Parse the JSON response and extract the new access token and expiration time
                var tokenData = JsonSerializer.Deserialize<Dictionary<string, string>>(data);
                // Check for missing keys
                if (tokenData == null || !tokenData.ContainsKey("access_token") || !tokenData.ContainsKey("expires_in"))
                {
                    throw new Exception("Failed to retrieve valid token data.");
                }

                var newAccessToken = tokenData["access_token"];
                var newExpirationTime = DateTime.UtcNow.AddSeconds(int.Parse(tokenData["expires_in"]));

                // Store the new access token and expiration time
                _httpContextAccessor.HttpContext?.Session.SetString("AccessToken", newAccessToken);
                _httpContextAccessor.HttpContext?.Session.SetString("ExpiresAt", newExpirationTime.ToString());

                return newAccessToken;
            }

            throw new Exception("Failed to refresh access token.");
        }

        private string GetRefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Session.GetString("RefreshToken");

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Refresh token is missing.");
            }

            return refreshToken;
        }

        public DateTime GetTokenExpirationTime()
        {
            var expirationTime = _httpContextAccessor.HttpContext?.Session.GetString("ExpiresAt");

            if (string.IsNullOrEmpty(expirationTime))
            {
                throw new InvalidOperationException("Token expiration time is missing.");
            }

            return DateTime.Parse(expirationTime);
        }
        // fetch songs by genre
        public async Task<JObject> GetSongsByGenreAsync(string genre)
        {
            var accessToken = await GetAccessTokenAsync();
            var endpoint = $"https://api.spotify.com/v1/recommendations?seed_genres={genre}&limit=10";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetStringAsync(endpoint);
            var jsonResponse = JObject.Parse(response);

            return jsonResponse;
        }
        // Fetch user playlists
        public async Task<JObject> GetUserPlaylistsAsync()
        {
            var accessToken = await GetAccessTokenAsync();
            var endpoint = "https://api.spotify.com/v1/me/playlists";
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetStringAsync(endpoint);
            var jsonResponse = JObject.Parse(response);
            return jsonResponse;

        }
    }
}