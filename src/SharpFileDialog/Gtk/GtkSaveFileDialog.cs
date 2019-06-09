using System;
using Gtk;

namespace SharpFileDialog.Gtk
{

    internal class GtkSaveFileDialog : ISaveFileDialogBackend
    {
        private FileChooserDialog _dialog;

        public GtkSaveFileDialog(string title)
        {
            _dialog = new FileChooserDialog(title,
                null,
                FileChooserAction.Save,
                "Cancel",
                ResponseType.Cancel,
                "Ok",
                ResponseType.Ok);
        }

        public void Dispose()
        {
            _dialog.Destroy();
        }

        public void Save(Action<DialogResult> callback)
        {
            _dialog.Run();
        }
    }
}