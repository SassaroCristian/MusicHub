﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MusicHub.Backend.Controllers
{
	public class AuthController : Controller
	{
		[HttpGet("login")]
		public IActionResult Login()
		{
			return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Spotify");
		}

		[HttpGet("callback")]
		public async Task<IActionResult> Callback()
		{
			var authenticateResult = await HttpContext.AuthenticateAsync("Spotify");
			if (!authenticateResult.Succeeded)
			{
                return Unauthorized();
            }
			var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
			var refreshToken = authenticateResult.Properties.GetTokenValue("refresh_token");

            HttpContext.Session.SetString("AccessToken", accessToken);
            HttpContext.Session.SetString("RefreshToken", refreshToken);
            return RedirectToAction("Index", "Home");
        }
	}
}

