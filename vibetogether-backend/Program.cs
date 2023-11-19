using PlayerHub;
using vibetogether_backend;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

