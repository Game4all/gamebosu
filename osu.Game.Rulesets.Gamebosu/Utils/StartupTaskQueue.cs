using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using osu.Framework.Logging;
using LogLevel = osu.Framework.Logging.LogLevel;

namespace osu.Game.Rulesets.Gamebosu.Utils
{
    /// <summary>
    /// Utility class for registering ruleset initialization tasks during game startup.
    /// </summary>
    internal static class StartupTaskQueue
    {
        // used for tracking whether the game has already completed startup.
        private static volatile bool gameFinishedStartup;
        private static volatile int numInstances;

        static StartupTaskQueue()
        {
            numInstances = 0;
            gameFinishedStartup = false;
        }

        /// <summary>
        /// Runs the startup tasks with a <seealso cref="StartupTaskAttribute"/>
        /// </summary>
        public static void RunStartupTasks(OsuGame game, GamebosuRuleset ruleset)
        {
            if (!gameFinishedStartup)
            {
                var startup_tasks = Assembly
                        .GetExecutingAssembly()
                        .GetTypes()
                        .Where(type => type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                          .Any(method => method.GetCustomAttributes(typeof(StartupTaskAttribute), false).Any()))
                        .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).Where(method => method.GetCustomAttributes(typeof(StartupTaskAttribute), false).Any()))
                        .OrderBy(method => (method.GetCustomAttributes(typeof(StartupTaskAttribute), false).First() as StartupTaskAttribute).Priority)
                        .AsEnumerable();

                foreach (var task in startup_tasks)
                {
                    try
                    {
                        task.Invoke(null, new object[] { game, ruleset });
                    }
                    catch (Exception e)
                    {
                        Logger.Log($"Failed to run startup task {task.DeclaringType.Name}:{task.Name} --> {e}", LoggingTarget.Runtime, LogLevel.Important);
                    }
                }

                gameFinishedStartup = true;
            }

            Interlocked.Increment(ref numInstances);
        }

        public static void FreeInstance()
        {
            Interlocked.Decrement(ref numInstances);

            if (numInstances == 0)
                gameFinishedStartup = false;
        }
    }
}
