using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{
    public class SaveFileDialog : ISaveFileDialogBackend
    {
        private ISaveFileDialogBackend _backend;
        public string DefaultFileName
        {
            get
            {
                return _backend.DefaultFileName;
            }
            set
            {
                _backend.DefaultFileName = value;
            }
        }

        public SaveFileDialog(string title = null)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _backend = new Win.WinSaveFileDialog(title);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    _backend = new Zenity.ZenitySaveFileDialog(title);
                }
                catch
                {
                    _backend = new Gtk.GtkSaveFileDialog(title);
                }
            }
        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Save(Action<DialogResult> callback, string filter = "All files(*.*) | *.*")
        {
            _backend.Save(callback, filter);
        }
    }
}
