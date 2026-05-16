using Cake.Core;
using Cake.Frosting;
using Microsoft.Extensions.DependencyInjection;

public class Program : IFrostingStartup
{
    public static int Main(string[] args)
    {
        // Create the host.
        var host = CakeHost.Create()
            .UseStartup<Program>();

        // Run the host.
        return host.Run(args);
    }

    public void Configure(IServiceCollection services)
    {
        services.UseContext<Context>();
        services.UseLifetime<Lifetime>();
        services.UseWorkingDirectory("..");
    }
}