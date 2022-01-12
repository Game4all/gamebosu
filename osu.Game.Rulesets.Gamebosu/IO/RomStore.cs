// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Gamebosu.IO
{
    public class RomStore : ResourceStore<EmulatedCartridge>
    {
        /// <summary>
        /// The rom storage.
        /// </summary>
        public Storage Storage { get; }

        private readonly Storage savesStorage;

        public static IEnumerable<string> RecognizedExtensions = new[]
        {
            ".gbc",
            ".gb",
            ".GB",
            ".GBC"
        };

        private const string save_file_extension = ".sav";

        public RomStore(Storage storage)
        {
            Storage = storage.GetStorageForDirectory("roms");
            savesStorage = this.Storage.GetStorageForDirectory("saves");

            foreach (var ext in RecognizedExtensions)
                AddExtension(ext);
        }

        public override EmulatedCartridge Get(string name)
        {
            foreach (var resName in GetFilenames(name))
            {
                try
                {
                    if (Storage.Exists(resName))
                    {
                        var cartStream = Storage.GetStream(resName);
                        var cartRom = new byte[cartStream.Length];
                        cartStream.Read(cartRom, 0, (int)cartStream.Length);

                        var saveStream = savesStorage.GetStream(resName + save_file_extension, FileAccess.ReadWrite, FileMode.OpenOrCreate);
                        return new EmulatedCartridge(cartRom, new StreamedExternalMemory(saveStream));
                    }
                }
                catch (System.Exception e)
                {
                    Logger.Log("Load of cartridge failed: " + e.ToString(), LoggingTarget.Runtime);
                    continue;
                }
            }

            return null;
        }

        public override Task<EmulatedCartridge> GetAsync(string name, CancellationToken token = default) => Task.Run(() => Get(name), token);

        public override IEnumerable<string> GetAvailableResources() => Storage.GetFiles(".")
                                                                               .ExcludeSystemFileNames()
                                                                               .Where(file => RecognizedExtensions.Any(ext => Path.GetExtension(file)?.Equals(ext, System.StringComparison.Ordinal) ?? false));
    }
}