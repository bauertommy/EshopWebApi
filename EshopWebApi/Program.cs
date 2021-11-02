using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EshopWebApi
{
    /// <summary>
    /// Start point to application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that runs
        /// </summary>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds web host
        /// </summary>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
