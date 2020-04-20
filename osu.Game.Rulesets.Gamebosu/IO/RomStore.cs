// gamebosu! ruleset. Copyright (c) Game4all 2020 Licensed under GPLv3.
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
        private readonly Storage storage;

        private readonly Storage savesStorage;

        public static IEnumerable<string> RECOGNIZED_EXTENSIONS = new[]
        {
            ".gbc",
            ".gb",
            ".GB",
            ".GBC"
        };

        public RomStore(Storage storage)
        {
            this.storage = storage.GetStorageForDirectory("roms");
            savesStorage  = this.storage.GetStorageForDirectory("saves");

            foreach (var ext in RECOGNIZED_EXTENSIONS)
                AddExtension(ext);
        }

        public override EmulatedCartridge Get(string name)
        {
            foreach (var res_name in GetFilenames(name))
            {
                try
                {
                    if (storage.Exists(res_name))
                    {
                        var cart_stream = storage.GetStream(res_name);
                        var cart_rom = new byte[cart_stream.Length];
                        cart_stream.Read(cart_rom, 0, (int)cart_stream.Length);

                        var save_stream = savesStorage.GetStream(res_name + ".sav", FileAccess.ReadWrite, FileMode.OpenOrCreate);
                        return new EmulatedCartridge(cart_rom, new StreamedExternalMemory(save_stream));
                    }
                }
                catch (System.Exception e)
                {
                    Logger.Error(e, "Load of cartridge failed", LoggingTarget.Runtime);
                    continue;
                }
            }

            return null;
        }

        public override Task<EmulatedCartridge> GetAsync(string name) => Task.Run(() => Get(name));

        public override IEnumerable<string> GetAvailableResources() => storage.GetFiles(".")
                                                                               .ExcludeSystemFileNames()
                                                                               .Where(file => RECOGNIZED_EXTENSIONS.Any(ext => Path.GetExtension(file)?.Equals(ext, System.StringComparison.Ordinal) ?? false));
    }
}
