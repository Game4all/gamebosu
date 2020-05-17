using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;
using System;

namespace osu.Game.Rulesets.Gamebosu.UI.Configuration
{
    public class DeleteDataDialog : PopupDialog
    {
        public DeleteDataDialog(Action action)
        {
            HeaderText = "Delete ROM save data?";
            BodyText = "Your precious ROM save files will be returned to void. Are you sure?";

            Icon = FontAwesome.Regular.TrashAlt;
            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"Yes. I'll start from zero again.",
                    Action = action
                },
                new PopupDialogCancelButton
                {
                    Text = @"No! Abort mission!",
                },
            };
        }
    }
}
