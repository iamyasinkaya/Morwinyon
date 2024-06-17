using Morwinyon.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRateLimiter<string>(100, TimeSpan.FromMinutes(1)); // 1 dakika içinde 100 istek
builder.Services.AddControllers();

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
