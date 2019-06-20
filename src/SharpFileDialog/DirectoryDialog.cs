using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{

    public class DirectoryDialog : IDirectoryDialogBackend
    {
        private IDirectoryDialogBackend _backend;

        public DirectoryDialog(string title = null)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _backend = new Win.WinDirectoryDialog(title);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    _backend = new Zenity.ZenityDirectoryDialog(title);
                }
                catch
                {
                    _backend = new Gtk.GtkDirectoryDialog(title);
                }
            }
        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Open(Action<DialogResult> callback)
        {
            _backend.Open(callback);
        }
    }
}
