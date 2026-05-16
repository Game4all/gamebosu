using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

public sealed class Lifetime : FrostingLifetime<Context>
{
    public override void Setup(Context context, ISetupContext info)
    {
    }

    public override void Teardown(Context context, ITeardownContext info)
    {
    }
}