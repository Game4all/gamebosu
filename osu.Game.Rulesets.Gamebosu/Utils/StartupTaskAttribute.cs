using System;


public sealed class StartupTaskAttribute : Attribute
{
    /// <summary>
    ///  The priority of this task. Tasks with lower values will be run first.
    /// </summary>
    public int Priority { get; set; } = 0;

    public StartupTaskAttribute()
    {
    }
}