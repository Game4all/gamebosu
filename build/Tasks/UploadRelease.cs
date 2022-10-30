using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Frosting;
using Octokit;

[TaskName("UploadRelease")]
[Dependency(typeof(FetchLazerVersion))]
[Dependency(typeof(BuildRelease))]
public sealed class UploadRelease : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.Information("Preparing for uploading ...");

        var token = context.Argument<string>("token");
        var repo = context.Argument<string>("repo");
        var user = context.Argument<string>("user");

        var client = new GitHubClient(new ProductHeaderValue(repo));
        client.Credentials = new Credentials(token);

        Task.Run(async () =>
        {
            var releases = await client.Repository.Release.GetAll(user, repo);
            if (releases.Any(rel => rel.TagName == context.ReleaseVersion))
            {
                context.Error("There's already an existing release with the given version number!");
                Environment.FailFast(null);
            }

            var release_data = new NewRelease(context.ReleaseVersion)
            {
                Name = $"{context.ReleaseVersion} release",
                Body = context.ReleaseBodyText.Replace("{RELEASE_VERSION}", context.ReleaseVersion)
                                              .Replace("{LAZER_VERSION}", context.RequiredLazerVersion),
            };

            context.Information("Creating release ....");

            var new_release = await client.Repository.Release.Create(user, repo, release_data);

            context.Information($"Release {context.ReleaseVersion} was created sucessfully!");

            var file = File.OpenRead(context.RulesetOutputPath);

            context.Information("Uploading asset .....");

            var upload = new ReleaseAssetUpload("osu.Game.Rulesets.Gamebosu.dll", "application/octet-stream", file, TimeSpan.FromSeconds(120));

            await client.Repository.Release.UploadAsset(new_release, upload);

            context.Information("Uploaded asset !");

        }).Wait();
    }
}