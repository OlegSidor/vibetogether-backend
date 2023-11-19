using VibeTogether.Authorization.Helpers;

namespace vibetogether_backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices()
        {
            ConnectionStringHelper.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

        }
    }
}
