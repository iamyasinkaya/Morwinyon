using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Morwinyon.Auth;
using Morwinyon.Auth.Tests.WebAPI.AuthService.Abstract;
using Morwinyon.Auth.Tests.WebAPI.AuthService.Concrete;
using Morwinyon.Auth.Tests.WebAPI.Data;
using Morwinyon.Auth.Tests.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));


// Varsayýlan JWT Authentication
builder.Services.AddJwtAuthentication();

//var jwtOptions = new JwtBearerOptions
//{
//    Key = "your_secret_key",
//    Issuer = "your_issuer",
//    Audience = "your_audience",
//    ValidateAudience = true,
//    ValidateLifetime = true,
//    ValidateIssuer = true,
//    ValidateIssuerSigningKey = true,
//    ClockSkew = TimeSpan.FromMinutes(5),
//    TokenExpiration = TimeSpan.FromMinutes(120),
//    SaveToken = true,
//    RoleClaimType = "role",
//    NameClaimType = "name",
//    RequireHttpsMetadata = true,
//    MetadataAddress = "https://your_metadata_address"
//};

//// Özelleþtirilmiþ JWT Authentication
//builder.Services.AddJwtAuthentication(jwtOptions);

//// Varsayýlan OAuth2 ve OpenID Connect Authentication
//builder.Services.AddOAuth2OpenIDConnectAuthentication();



//var oauth2Options = new OAuth2Options
//{
//    ClientId = "your_client_id",
//    ClientSecret = "your_client_secret",
//    Authority = "https://your_authority",
//    ResponseType = "code",
//    SaveTokens = true,
//    RequireHttpsMetadata = true
//};

//// Özelleþtirilmiþ OAuth2 ve OpenID Connect Authentication
//builder.Services.AddOAuth2OpenIDConnectAuthentication(oauth2Options);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
