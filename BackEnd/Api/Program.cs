using Business.ConnectSpotify;
using Business.GetterSpotify;
using SpotifyApi.AuthSpotify;
using Core.Services;
using Core.SpotifyApi.IAuthSpotify;
using Core.SpotifyApi.IInputSpotify;
using SpotifyApi.InputSpotify;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Core.Services.ICommentary;
using Business.CommentaryBusiness;
using Core.Repositories.ILikeDataBaseAccess;
using Business.LikeBusiness;
using Core.Repositories.ICommentaryDataBaseAccess;
using Core.Services.ILikeBusiness;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IConnectSpotify,ConnectSpotify>();
builder.Services.AddScoped<IGetterSpotify,GetterSpotify>();
builder.Services.AddScoped<IAuthSpotify,AuthSpotify>();
builder.Services.AddScoped<IInputSpotify, InputSpotify>();
builder.Services.AddScoped<ICommentary, CommentaryBusiness>();
builder.Services.AddScoped<ICommentaryDataBaseAccess, CommentaryDataBaseAccess>();
builder.Services.AddScoped<CommentifyContext, CommentifyContext>();
builder.Services.AddScoped<ILikeDataBaseAccess, LikeDataBaseAccess>();
builder.Services.AddScoped<IlikeBusiness, LikeBusiness>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddDbContext<CommentifyContext>(options =>
    options.UseSqlite("Data Source=../Repositories/DataBase/spotifyapp.db"));

   //  Cors
builder.Services.AddCors(
    p => p.AddPolicy(name: "AllowedOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    }));

    // Stokage dans la session
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>  
{  
    options.Cookie.Name = ".AspNetCore.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseRouting();

app.UseCors("AllowedOrigins");
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
