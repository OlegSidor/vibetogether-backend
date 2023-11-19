using PlayerHub;
using PlayerHub.Interfaces;

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
        }
    }
}
