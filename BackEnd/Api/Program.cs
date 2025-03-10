using Business;
using SpotifyApi.AuthSpotify;
using Core.Services;
using Core.SpotifyApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IConnectSpotify,ConnectSpotify>();
builder.Services.AddScoped<IAuthSpotify,AuthSpotify>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddControllers();

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


app.UseSession();
app.UseCors("AllowedOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
