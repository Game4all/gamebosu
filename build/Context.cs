using System;
using Cake.Core;
using Cake.Frosting;
using System.IO;

public class Context : FrostingContext
{
    public Context(ICakeContext context) 
        : base(context)
    {
        var date = DateTime.Now;
        ReleaseVersion = $"{date.Year}.{date.Month}{date.Day.ToString("#00")}.{0}";
        ReleaseBodyText = File.ReadAllText("ReleaseHeader.md");
    }

    public readonly string ReleaseVersion;

    public readonly string ReleaseBodyText;

    public const string RULESET_PROJECT_PATH = "./osu.Game.Rulesets.Gamebosu";

    public const string RULESET_OUTPUT_PATH = RULESET_PROJECT_PATH + "/bin/Release/netstandard2.1/osu.Game.Rulesets.Gamebosu.dll";
}