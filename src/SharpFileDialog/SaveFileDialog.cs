using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{
    public class SaveFileDialog : ISaveFileDialogBackend
    {
        private ISaveFileDialogBackend _backend;

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
                    _backend = new Gtk.GtkSaveFileDialog(title);
                }
                catch
                {
                    _backend = new Zenity.ZenitySaveFileDialog(title);
                }
            }

        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Save(string filter, Action<DialogResult> callback)
        {
            _backend.Save(filter, callback);
        }
    }
}
