using api.Data;
using api.Models;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services;
using api.Services.Interfaces;
using api.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add authentication (Cookie + Google)
builder.Services.AddAuthentication(options =>
{
    // Google as the default challenge scheme
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(
//     options =>
// {
//     options.Cookie.SameSite = SameSiteMode.Lax;
//     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
// }
)
.AddGoogle(googleOptions =>
{
    var clientId = builder.Configuration["Authentication:Google:ClientId"];
    var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

    if (string.IsNullOrEmpty(clientId))
        throw new ArgumentNullException(nameof(clientId), "Google ClientId not configured.");
    if (string.IsNullOrEmpty(clientSecret))
        throw new ArgumentNullException(nameof(clientSecret), "Google ClientSecret not configured.");

    googleOptions.ClientId = clientId;
    googleOptions.ClientSecret = clientSecret;
    googleOptions.Scope.Add("openid");
    googleOptions.Scope.Add("profile");
    googleOptions.Scope.Add("email");
    googleOptions.SaveTokens = false; // not require access & refresh token
    googleOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// Configure EF Core
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<OpenWeatherMapSettings>(
    builder.Configuration.GetSection("OpenWeatherMap"));

// Dependency injection
builder.Services.AddSingleton<ICityService, CityService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register WeatherService with HttpClient
builder.Services.AddHttpClient<IWeatherService, WeatherService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<OpenWeatherMapSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Enable in-memory caching
builder.Services.AddMemoryCache();

var app = builder.Build();

// // Middleware pipeline
// app.UseCookiePolicy(new CookiePolicyOptions
// {
//     MinimumSameSitePolicy = SameSiteMode.Lax,
//     Secure = CookieSecurePolicy.Always
// });

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();
