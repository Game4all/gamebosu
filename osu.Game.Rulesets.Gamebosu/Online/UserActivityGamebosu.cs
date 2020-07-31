using osu.Game.Graphics;
using osu.Game.Users;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Gamebosu.Online
{
    public abstract class UserActivityGamebosu : UserActivity
    {
        public abstract string StatusString { get; }

        public override string Status => "gamebosu! : " + StatusString;

        public override Color4 GetAppropriateColour(OsuColour colours) => colours.Cyan;
    }

    public class UserActivityGamebosuChoosing : UserActivityGamebosu
    {
        public override string StatusString => "choosing a ROM ";
    }

    public class UserActivityGamebosuPlaying : UserActivityGamebosu
    {
        private string rom;

        public UserActivityGamebosuPlaying(string str)
        {
            rom = str;
        }

        public override string StatusString => "playing " + rom;
    }
}
