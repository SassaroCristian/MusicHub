using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth; 
using Microsoft.EntityFrameworkCore;
using MusicHub.Backend.Data;
using MusicHub.Backend.Models;
using MusicHub.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add other services
builder.Services.AddSingleton<SpotifyService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120); // Session expiration time
});

builder.Services.Configure<SpotifySettings>(
    builder.Configuration.GetSection("Spotify")
);
// Add Authentication with Spotify OAuth
builder.Services.AddAuthentication(options =>
{
    // Default schemes for cookies and Spotify authentication
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Spotify"; // Specify the "Spotify" authentication scheme here
})
.AddSpotify(options =>
{
    options.ClientId = builder.Configuration["Spotify:ClientId"] ?? throw new ArgumentNullException("Spotify:ClientId configuration is missing.");
    options.ClientSecret = builder.Configuration["Spotify:ClientSecret"] ?? throw new ArgumentNullException("Spotify:ClientSecret configuration is missing.");

    // Define the callback path (equivalent to RedirectUri in newer versions)
    options.CallbackPath = "/callback"; // Match the path in your Spotify app settings
})
.AddCookie();  // Add the cookie-based authentication handler

var app = builder.Build();

// Configure the HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Use Authentication and Authorization middleware
app.UseAuthentication(); // Ensure authentication is added
app.UseAuthorization();  // Ensure authorization is added

// Define the default route for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();