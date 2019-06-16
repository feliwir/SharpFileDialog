using System;
using System.Threading.Tasks;
using Gtk;

namespace SharpFileDialog.Gtk
{
    internal class GtkSaveFileDialog : ISaveFileDialogBackend
    {
        private FileChooserDialog _dialog;

        public GtkSaveFileDialog(string title)
        {
            GtkUtil.Initialize();

            _dialog = new FileChooserDialog(title,
                null,
                FileChooserAction.Save,
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

        public void Save(string filterString, Action<DialogResult> callback)
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