// gamebosu! ruleset. Copyright Lucas ARRIESSE aka Game4all. Licensed under GPLv3.
// See LICENSE at root of repo for more information on licensing.

using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Rulesets.Gamebosu.UI.Configuration
{
    public partial class DeleteDataErrorDialog : PopupDialog
    {
        public DeleteDataErrorDialog()
        {
            HeaderText = "An error occured while trying to delete ROM save data...";
            Icon = FontAwesome.Solid.Exclamation;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"Ok",
                },
            };
}
    }
}