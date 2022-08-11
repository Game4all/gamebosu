using System.Threading;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Rulesets.Gamebosu.Utils
{
    /// <summary>
    /// Utility class for loading and managing ruleset resources during the game's lifetime.
    /// </summary>
    internal static class ResourceLoaderUtils
    {
        private static volatile int numInstances;

        static ResourceLoaderUtils()
        {
            numInstances = 0;
        }

        /// <summary>
        /// Ensuires the ruleset resources are loaded and available to the whole game.
        /// </summary>
        public static void EnsureResourcesLoaded(TextureStore store, Ruleset ruleset)
        {
            if (numInstances == 0) 
                store.AddTextureSource(new TextureLoaderStore(ruleset.CreateResourceStore()));

            Interlocked.Increment(ref numInstances);
        }

        public static void FreeResources() => Interlocked.Decrement(ref numInstances);
    }
}
