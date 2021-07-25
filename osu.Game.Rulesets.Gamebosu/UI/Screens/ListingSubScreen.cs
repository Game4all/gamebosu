// gamebosu! ruleset. Copyright Lucas A. aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Gamebosu.IO;
using osu.Game.Rulesets.Gamebosu.UI.Screens.Listing;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens
{
    [Cached]
    public class ListingSubScreen : GamebosuSubScreen
    {
        private readonly RomListing listing;

        private readonly RomImportHandler romImportHandler;

        private readonly WaveContainer waveContainer;

        [Resolved]
        private RomStore roms { get; set; }

        [Resolved(CanBeNull = true)]
        private OsuGame game { get; set; }

        private ScheduledDelegate refreshDelegate;

        public ListingSubScreen()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;

            var backgroundColour = Color4Extensions.FromHex(@"3e3a44");

            InternalChild = waveContainer = new WaveContainer
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    romImportHandler = new RomImportHandler(),
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = backgroundColour,
                    },
                    listing = new RomListing
                    {
                        RelativeSizeAxes = Axes.Both,
                        RomSelected = Prepare,
                        Padding = new MarginPadding { Top = ListingHeader.HEIGHT },
                    },
                    new ListingHeader()
                }
            };
        }

        /// <summary>
        /// Triggers a refresh of the rom listing.
        /// </summary>
        public void Refresh()
        {
            refreshDelegate?.Cancel();
            listing.AvailableRoms = roms.GetAvailableResources();
            refreshDelegate = Scheduler.AddDelayed(Refresh, 2500);
        }

        protected void Prepare(string clicked)
        {
            roms.GetAsync(clicked).ContinueWith(rom => 
            {
                Schedule(() => this.Push(new GameplaySubScreen(rom.Result)));
            });
        }

        public override void OnEntering(IScreen last)
        {
            game?.RegisterImportHandler(romImportHandler);
            waveContainer.Show();
            Refresh();
        }

        public override void OnSuspending(IScreen next)
        {
            waveContainer.Hide();
            base.OnSuspending(next);
        }

        public override void OnResuming(IScreen last)
        {
            waveContainer.Show();
            base.OnResuming(last);
        }

        public override bool OnExiting(IScreen next)
        {
            game.UnregisterImportHandler(romImportHandler);
            waveContainer.Hide();
            return base.OnExiting(next);
        }
    }
}