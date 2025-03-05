using Business;
using SpotifyApi.AuthSpotify;
using Core.Services;
using Core.SpotifyApi;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConnectSpotify,ConnectSpotify>();
builder.Services.AddScoped<IAuthSpotify,AuthSpotify>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddControllers();

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
