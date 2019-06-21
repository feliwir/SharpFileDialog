using System;
using Gtk;

namespace SharpFileDialog.Gtk
{

    internal class GtkDirectoryDialog : IDirectoryDialogBackend
    {
        private FileChooserDialog _dialog;

        public GtkDirectoryDialog(string title)
        {
            GtkUtil.Initialize();

            _dialog = new FileChooserDialog(title,
                null,
                FileChooserAction.SelectFolder,
                "Cancel",
                ResponseType.Cancel,
                "Ok",
                ResponseType.Ok);

            while (Application.EventsPending())
                Application.RunIteration();
        }

        public void Dispose()
        {
            _dialog.Destroy();
        }

        public void Open(Action<DialogResult> callback)
        {
            if (_dialog.Run() == (int)ResponseType.Ok)
            {
                callback(new DialogResult()
                {
                    FileName = _dialog.Filename,
                    Success = true
                });
            }
            _dialog.Destroy();

            while (Application.EventsPending())
                Application.RunIteration();
        }
    }
}