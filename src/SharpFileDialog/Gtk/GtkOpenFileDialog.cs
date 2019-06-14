using System;
using System.Threading.Tasks;
using Gtk;

namespace SharpFileDialog.Gtk
{

    internal class GtkOpenFileDialog : IOpenFileDialogBackend
    {
        private FileChooserDialog _dialog;

        public GtkOpenFileDialog(string title)
        {
            GtkUtil.Initialize();

            _dialog = new FileChooserDialog(title,
                null,
                FileChooserAction.Open,
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

        public void Open(string filter, Action<DialogResult> callback)
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