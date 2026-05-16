using Cake.Frosting;

[TaskDescription("Default target that runs the release build.")]
[IsDependentOn(typeof(BuildRelease))]
public sealed class Default : FrostingTask<Context>
{
}