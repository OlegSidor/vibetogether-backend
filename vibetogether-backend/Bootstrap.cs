using PlayerHub.Interfaces;
using VibeTogether.Authorization.JWT;
using VibeTogether.Authorization.Services;
using vibetogether_backend.Context;
using vibetogether_backend.Services;

namespace vibetogether_backend
{
    public static class Bootstrap
    {
        public static void DependencyIndection(IServiceCollection services)
        {
            //SignalR
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddSingleton<IRoomManager, RoomManager>();
            services.AddSingleton<IJwtHelper, JwtHelper>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IRoomService, RoomService>();

        }
    }
}
