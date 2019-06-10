using System;
using Gtk;

namespace SharpFileDialog.Gtk
{

    internal class GtkOpenFileDialog : IOpenFileDialogBackend
    {
        private FileChooserDialog _dialog;

        public GtkOpenFileDialog(string title)
        {
            Application.Init();

            _dialog = new FileChooserDialog("title",
                null,
                FileChooserAction.Open,
                "Cancel",
                ResponseType.Cancel,
                "Ok",
                ResponseType.Ok);

            _dialog.ShowAll();

            Application.Run();
        }

        public void Dispose()
        {
            _dialog.Destroy();
        }

        public void Open(string filter, Action<DialogResult> callback)
        {
            _dialog.Run();
        }
    }
}