// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Database;
using osu.Game.Rulesets.Gamebosu.IO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Listing
{
    /// <summary>
    /// Handles import of GB / GBC files.
    /// </summary>
    public class RomImportHandler : Component, ICanAcceptFiles
    {
        [Resolved]
        private ListingSubScreen listing { get; set; }

        [Resolved]
        private RomStore store { get; set; }

        public IEnumerable<string> HandledExtensions => RomStore.RECOGNIZED_EXTENSIONS;

        public Task Import(params string[] paths) => Import(paths.Select(path => new ImportTask(path)).ToArray());

        public Task Import(params ImportTask[] tasks) => Task.Run(() =>
        {
            foreach (var task in tasks)
            {
                var file = new FileInfo(task.Path);
                file.CopyTo(store.Storage.GetFullPath(file.Name));
            }

            Schedule(listing.Refresh);
        });
    }
}