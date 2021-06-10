using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Frosting;

[TaskName("RestoreProject")]
public sealed class RestoreProject : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Restoring project dependencies....");
        context.DotNetCoreRestore(context.RulesetProjectPath);
    }
}