// gamebosu! ruleset. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu.UI.Screens.Selection
{
    public class SelectionCard : CompositeDrawable
    {
        private readonly Sprite cartridge;
        private readonly SpriteIcon loadFailedIcon;
        private readonly OsuSpriteText loadFailedText;
        private readonly OsuSpriteText romNameText;

        public SelectionCard(string romName)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
            Masking = true;
            CornerRadius = 15;

            InternalChild = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Gray.Opacity(0.4f)
                    },
                    new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.Y,
                        AutoSizeAxes = Axes.X,
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Direction = FillDirection.Vertical,
                        Spacing = new osuTK.Vector2(0, 0.10f),
                        Children = new Drawable[]
                        {
                            new Container
                            {
                                Masking = true,
                                RelativeSizeAxes = Axes.Y,
                                Height = 0.75f,
                                Width = 300,
                                Margin = new MarginPadding { Horizontal =  10, Vertical = 10 },
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = Color4.Black.Opacity(0.6f)
                                    },
                                    loadFailedText = new OsuSpriteText
                                    {
                                        Anchor = Anchor.BottomCentre,
                                        Origin = Anchor.BottomCentre,
                                        Margin = new MarginPadding { Bottom = 10 },
                                        Font = OsuFont.GetFont(Typeface.Torus, 20, FontWeight.Bold),
                                        Text = "Failed to load cartridge!",
                                        Colour = Color4.Red,
                                        Alpha = 0,
                                    },
                                    cartridge = new Sprite
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Scale = new osuTK.Vector2(2)
                                    },
                                    loadFailedIcon = new SpriteIcon
                                    {
                                        Icon = OsuIcon.CrossCircle,
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Colour = Color4.Red.Opacity(0.9f),
                                        Size = new osuTK.Vector2(160),
                                        Alpha = 0
                                    },
                                },
                                CornerRadius = 15
                            },
                            new Container
                            {
                                Anchor = Anchor.TopLeft,
                                Origin = Anchor.TopLeft,
                                Width = 300,
                                RelativeSizeAxes = Axes.Y,
                                Height = 0.10f,
                                Margin = new MarginPadding { Horizontal =  10, Vertical = 10 },
                                Child = romNameText = new OsuSpriteText
                                {
                                    Font = OsuFont.GetFont(Typeface.Torus, 28, FontWeight.Bold),
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Text = romName,
                                }
                            }
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            cartridge.Texture = textures?.Get("cartridge");
        }

        public void MarkUnavalaible()
        {
            loadFailedIcon
                .FadeIn(250, Easing.In)
                .Then(800)
                .FadeOut(400, Easing.Out);

            loadFailedText
                .FadeIn(250, Easing.In)
                .Then(800)
                .FadeOut(400, Easing.Out);

            romNameText
                .FadeColour(Color4.Red, 250, Easing.In)
                .Then(800)
                .FadeColour(Color4.White, 400, Easing.Out);
        }
    }
}