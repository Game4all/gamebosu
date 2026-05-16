using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNet.Build;

[TaskDescription("Builds the ruleset in Release mode with the current release version.")]
[TaskName("BuildRelease")]
[IsDependentOn(typeof(RestoreProject))]
public sealed class BuildRelease : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Cleaning previous build artifacts ...");

        context.DotNetClean(context.RulesetProjectPath);

        context.Information($"Building release version {context.ReleaseVersion}");

        var msbuildOpts = new DotNetMSBuildSettings();
        msbuildOpts.SetVersion(context.ReleaseVersion);

        var buildOpts = new DotNetBuildSettings {
            Configuration = "Release",
            MSBuildSettings = msbuildOpts
        };

        context.DotNetBuild(context.RulesetProjectPath, buildOpts);

        context.Information("Release built sucessfully");
    }
}