using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayerHub;
using VibeTogether.Authorization.Data;
using VibeTogether.Authorization.Helpers;
using VibeTogether.Authorization.Models;
using vibetogether_backend;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var config = builder.Configuration;
var startup = new Startup(config);
startup.ConfigureServices();

//signalr cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("signalr",
                        builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()

                        .AllowCredentials()
                        .SetIsOriginAllowed(hostName => true));
});

builder.Services.AddControllers();

builder.Services.AddDbContext<VibeTogetherDbContext>(options =>
    options.UseSqlServer(ConnectionStringHelper.ConnectionString));
builder.Services.AddIdentity<VibeUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<VibeTogetherDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


Bootstrap.DependencyIndection(builder.Services);


var app = builder.Build();

app.UseCors("signalr");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

//Signalar Hub
app.MapHub<Player>("/player");


app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HealthCheck}/{action=Index}");


app.Run();

