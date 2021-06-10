
using System.Diagnostics;
using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Frosting;

[Dependency(typeof(RestoreProject))]
public sealed class FetchLazerVersion : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Fetching lazer version...");

        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"list {context.RulesetProjectPath} package",
            RedirectStandardOutput = true
        });

        var output = process.StandardOutput.ReadToEnd();

        //[0] is package name
        //[1] is package version        
        var parsed_package_version_info = output[(output.IndexOf('>') + 1)..].Split(' ').Where(str => !string.IsNullOrWhiteSpace(str));

        context.RequiredLazerVersion = parsed_package_version_info.ElementAt(1);
    }
}