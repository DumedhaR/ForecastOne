using api.Data;
using api.Repositories;
using api.Repositories.Interfaces;
using api.Services;
using api.Services.Interfaces;
using api.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(googleOptions =>
{
    var clientId = builder.Configuration["Authentication:Google:ClientId"];
    var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    if (clientId == null)
    {
        throw new ArgumentNullException(nameof(clientId));
    }
    if (clientSecret == null)
    {
        throw new ArgumentNullException(nameof(clientSecret));
    }

    googleOptions.ClientId = clientId;
    googleOptions.ClientSecret = clientSecret;
    googleOptions.Scope.Add("openid");
    googleOptions.Scope.Add("profile");
    googleOptions.Scope.Add("email");
});


// Add IHttpClient
builder.Services.Configure<OpenWeatherMapSettings>(
    builder.Configuration.GetSection("OpenWeatherMap"));

// Config dependency injection
builder.Services.AddSingleton<ICityService, CityService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IWeatherService, WeatherService>();

// Register WeatherService with typed HttpClient
builder.Services.AddHttpClient<IWeatherService, WeatherService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<OpenWeatherMapSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Enable Memory Caching
builder.Services.AddMemoryCache();

// Add Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ForecastOne API",
        Version = "v1",
        Description = "ForecastOne API with minimal API, controllers, EF Core, and Swagger"
    });
});

// Configure EF Core
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForecastOne API v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

// Enable controller endpoints
app.MapControllers();

app.Run();
