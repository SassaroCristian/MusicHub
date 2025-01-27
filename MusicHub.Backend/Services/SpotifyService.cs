public class SpotifyService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;

    public SpotifyService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;
    }

    public string GetAccessToken()
    {
        var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");

        if (string.IsNullOrEmpty(accessToken))
        {
            throw new InvalidOperationException("Access token is missing.");
        }

        // Check if the token is expired, and if so, refresh it
        var expirationTime = GetTokenExpirationTime();
        if (DateTime.UtcNow > expirationTime)
        {
            accessToken = RefreshAccessToken();
        }

        return accessToken;
    }

    private string RefreshAccessToken()
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

        var response = _httpClient.PostAsync("https://accounts.spotify.com/api/token", requestContent).Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            // Parse the JSON response and extract the new access token and expiration time
            var tokenData = JsonSerializer.Deserialize<Dictionary<string, string>>(data);
            var newAccessToken = tokenData["access_token"];
            var newExpirationTime = DateTime.UtcNow.AddSeconds(int.Parse(tokenData["expires_in"]));

            // Store the new access token and expiration time
            _httpContextAccessor.HttpContext.Session.SetString("AccessToken", newAccessToken);
            _httpContextAccessor.HttpContext.Session.SetString("ExpiresAt", newExpirationTime.ToString());

            return newAccessToken;
        }

        throw new Exception("Failed to refresh access token.");
    }

    private string GetRefreshToken()
    {
        return _httpContextAccessor.HttpContext.Session.GetString("RefreshToken");
    }

    public DateTime GetTokenExpirationTime()
    {
        var expirationTime = _httpContextAccessor.HttpContext.Session.GetString("ExpiresAt");

        if (string.IsNullOrEmpty(expirationTime))
        {
            throw new InvalidOperationException("Token expiration time is missing.");
        }

        return DateTime.Parse(expirationTime);
    }
}