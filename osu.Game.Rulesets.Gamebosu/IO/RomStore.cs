// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using Emux.GameBoy.Cartridge;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static IEnumerable<string> RECOGNIZED_EXTENSIONS = new[]
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

            foreach (var ext in RECOGNIZED_EXTENSIONS)
                AddExtension(ext);
        }

        public override EmulatedCartridge Get(string name)
        {
            foreach (var res_name in GetFilenames(name))
            {
                try
                {
                    if (Storage.Exists(res_name))
                    {
                        var cart_stream = Storage.GetStream(res_name);
                        var cart_rom = new byte[cart_stream.Length];
                        cart_stream.Read(cart_rom, 0, (int)cart_stream.Length);

                        var save_stream = savesStorage.GetStream(res_name + save_file_extension, FileAccess.ReadWrite, FileMode.OpenOrCreate);
                        return new EmulatedCartridge(cart_rom, new StreamedExternalMemory(save_stream));
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

        public override Task<EmulatedCartridge> GetAsync(string name) => Task.Run(() => Get(name));

        public override IEnumerable<string> GetAvailableResources() => Storage.GetFiles(".")
                                                                               .ExcludeSystemFileNames()
                                                                               .Where(file => RECOGNIZED_EXTENSIONS.Any(ext => Path.GetExtension(file)?.Equals(ext, System.StringComparison.Ordinal) ?? false));
    }
}