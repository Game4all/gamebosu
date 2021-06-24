using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Common.Tools.DotNetCore.Build;

[TaskName("BuildRelease")]
[Dependency(typeof(RestoreProject))]
public sealed class BuildRelease : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Cleaning previous build artifacts ...");

        context.DotNetCoreClean(context.RulesetProjectPath);

        context.Information($"Building release version {context.ReleaseVersion}");

        var msbuildOpts = new DotNetCoreMSBuildSettings();
        msbuildOpts.SetVersion(context.ReleaseVersion);

        var buildOpts = new DotNetCoreBuildSettings {
            Configuration = "Release",
            MSBuildSettings = msbuildOpts
        };

        context.DotNetCoreBuild(context.RulesetProjectPath, buildOpts);

        context.Information("Release built sucessfully");
    }
}