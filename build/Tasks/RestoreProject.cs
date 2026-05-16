using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNet;
using Cake.Frosting;

[TaskDescription("Restores NuGet packages for the ruleset project.")]
[TaskName("RestoreProject")]
public sealed class RestoreProject : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Restoring project dependencies....");
        context.DotNetRestore(context.RulesetProjectPath);
    }
}