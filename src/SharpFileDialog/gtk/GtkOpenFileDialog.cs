

using System;
using Gtk;

namespace SharpFileDialog
{
    internal class GtkFileDialog : IOpenFileDialogBackend
    {
        private Gtk.FileChooserDialog _dialog;

        public GtkFileDialog()
        {
            _dialog = new Gtk.FileChooserDialog("Please open a file",
                null,
                FileChooserAction.Open,
                "Cancel",
                Gtk.ResponseType.Cancel,
                "Ok",
                Gtk.ResponseType.Ok);
        }

        public void Dispose()
        {
            _dialog.Destroy();
        }

        public void Open(string filter, Action<bool> callback)
        {
            _dialog.Run();
        }
    }
}