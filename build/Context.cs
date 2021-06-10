using System;
using Cake.Core;
using Cake.Frosting;
using System.IO;

public class Context : FrostingContext
{
    private const string ruleset_project_name = "osu.Game.Rulesets.Gamebosu";

    public string ReleaseVersion => DateTime.Now.ToString("yyyy.Mdd.0");

    public string ReleaseBodyText => File.ReadAllText("build/ReleaseHeader.md");

    public string RequiredLazerVersion { get; set; }

    public Context(ICakeContext context)
        : base(context)
    {
    }

    private string ruleset_project_csproj_path => Path.Combine("..\\", ruleset_project_name, ruleset_project_name + ".csproj");

    public string RulesetProjectPath => Path.Combine(".\\", ruleset_project_name);

    public string RulesetOutputPath => Path.Combine(RulesetProjectPath, "bin/Release/netstandard2.1/osu.Game.Rulesets.Gamebosu.dll");
}