using System;
using Cake.Core;
using Cake.Frosting;
using System.IO;

public class Context : FrostingContext
{
    private const string ruleset_project_name = "osu.Game.Rulesets.Gamebosu";

    public readonly string ReleaseVersion;

    public readonly string ReleaseBodyText;

    public Context(ICakeContext context)
        : base(context)
    {
        var date = DateTime.Now;
        ReleaseVersion = $"{date.Year}.{date.Month}{date.Day.ToString("#00")}.{0}";
        ReleaseBodyText = File.ReadAllText("ReleaseHeader.md");
    }

    private string ruleset_project_csproj_path => Path.Combine("..\\", ruleset_project_name, ruleset_project_name + ".csproj");

    public string RulesetProjectPath => Path.Combine(".\\", ruleset_project_name);

    public string RulesetOutputPath => Path.Combine(RulesetProjectPath, "bin/Release/netstandard2.1/osu.Game.Rulesets.Gamebosu.dll");
}