using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using osu.Framework.Testing;

namespace osu.Game.Rulesets.Gamebosu.Utils
{
    /// <summary>
    /// Utility class for registering ruleset initialization tasks during game startup.
    /// </summary>
    [ExcludeFromDynamicCompile]
    internal static class StartupTaskQueue
    {
        // used for tracking whether the game has already completed startup.
        private static volatile bool gameFinishedStartup;
        private static volatile int numInstances;

        private static readonly List<Action<OsuGame, GamebosuRuleset>> startup_tasks;

        static StartupTaskQueue()
        {
            startup_tasks = new List<Action<OsuGame, GamebosuRuleset>>();
            numInstances = 0;
            gameFinishedStartup = false;
        }

        /// <summary>
        /// Enqueues a task to be ran once at game startup.
        /// </summary>
        /// <param name="task"></param>
        public static void EnqueueStartupTask(Action<OsuGame, GamebosuRuleset> task)
        {
            if (!gameFinishedStartup)
                startup_tasks.Add(task);
        }

        /// <summary>
        /// Runs the registered startup tasks.
        /// </summary>
        public static void RunStartupTasks(OsuGame game, GamebosuRuleset ruleset)
        {
            if (!gameFinishedStartup)
            {
                startup_tasks.ForEach(task => task(game, ruleset));

                Debug.WriteLine("");

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
