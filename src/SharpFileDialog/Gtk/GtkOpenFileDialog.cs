using System;
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

        public void Open(Action<DialogResult> callback, string filterString)
        {
            foreach (var item in GtkUtil.ConvertFilter(filterString))
            {
                var filter = new FileFilter();
                filter.Name = item.Name;
                filter.AddPattern(item.Pattern);

                _dialog.AddFilter(filter);
            }

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