using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
namespace MusicHub.Backend.Controllers
{
	public class AccountController : Controller
	{
        // Login route: Initiates the Spotify authentication process
        [HttpGet("login")]
		public IActionResult Login()
		{
            // Redirect to Spotify's authorization page
            return Challenge(new AuthenticationProperties
			{
				RedirectUri = "/home" // Where to redirect users after successful login
            }, "Spotify"); // Specify the authentication scheme ("Spotify")
        }
        // Callback route: Handles Spotify's response
        [HttpGet("callback")]
		public async Task<IActionResult> Callback()
		{
			var authenticateResult = await HttpContext.AuthenticateAsync("Spotify");

			if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
			{
                // Authentication failed; redirect to an error page or retry
                return Redirect("/error");
			}

            // Access the Spotify token
            var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
            var refreshToken = authenticateResult.Properties.GetTokenValue("refresh_Token");
            var expirationTime = authenticateResult.Properties.GetTokenValue("expires_at");

            // Store the tokens in session or database (example here stores them in session)
            HttpContext.Session.SetString("AccessToken", accessToken ?? string.Empty);
            HttpContext.Session.SetString("RefreshToken", refreshToken ?? string.Empty);
            HttpContext.Session.SetString("ExpirationTime", expirationTime ?? string.Empty);
            // You can store the token or redirect users to the main app
            return RedirectToAction("Index", "Home");
		}
	}
}

