using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using Cake.Common.Tools.DotNetCore.MSBuild;

[TaskName("BuildRelease")]
public sealed class BuildRelease : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Preparing for building ...");

        context.DotNetCoreClean(Context.RULESET_PROJECT_PATH);

        context.Information($"Building release version {context.ReleaseVersion}");

        var settings = new DotNetCoreMSBuildSettings();
        settings.SetConfiguration("Release");
        settings.SetVersion(context.ReleaseVersion);
        context.DotNetCoreMSBuild(Context.RULESET_PROJECT_PATH, settings);

        context.Information("Release built sucessfully");
    }
}