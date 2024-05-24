using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using weatherApp.Data;
using weatherApp.Data.Repositories.CityRepository;
using weatherApp.Data.Repositories.FollowedCityRepository;
using weatherApp.Mappers;
using weatherApp.Services.CityService;
using weatherApp.Services.FavouriteCityService;
using weatherApp.Services.FollowedCityService;
using weatherApp.Services.UserService;
using weatherApp.Services.weatherService;
using weatherApp.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<OpenWeatherSettings>(builder.Configuration.GetSection("OpenWeatherSettings"));
var authSettings = builder.Configuration.GetSection("Authentication");

builder.Services.AddDbContext<WeatherAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = authSettings["Authority"];
    o.Audience = authSettings["Audience"];
    o.SaveToken = true;
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = "permission"
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(authSettings["Swagger:AuthorizationUrl"]),
                TokenUrl = new Uri(authSettings["Swagger:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "Open ID" }
                }
            }
        }
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List<string> { "openid" }
        }
    });
});


builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IFavouriteCityRepository, FavouriteCityRepository>();
builder.Services.AddScoped<IFavouriteCityService, FavouriteCityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFollowedCityRepository, FollowedCityRepository>();
builder.Services.AddScoped<IFollowedCityService, FollowedCityService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId("app");
        c.OAuthAppName("WeatherApp");
        c.OAuthUsePkce();
    }
);


app.UseCors("AllowAll");

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();