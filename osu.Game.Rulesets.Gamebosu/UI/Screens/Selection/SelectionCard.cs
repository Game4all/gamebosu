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
                                    cartridge = new Sprite
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Scale = new osuTK.Vector2(2)
                                    }
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
                                Child = new OsuSpriteText
                                {
                                    Font = OsuFont.GetFont(Typeface.Torus, 24, FontWeight.Bold),
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
    }
}