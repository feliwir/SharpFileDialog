using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{

    public class OpenFileDialog : IOpenFileDialogBackend
    {
        private IOpenFileDialogBackend _backend;

        public OpenFileDialog(string title = null)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _backend = new Win.WinOpenFileDialog(title);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    _backend = new Zenity.ZenityOpenFileDialog(title);
                }
                catch
                {
                    _backend = new Gtk.GtkOpenFileDialog(title);
                }
            }
        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Open(Action<DialogResult> callback, string filter = "All files(*.*) | *.*")
        {
            _backend.Open(callback, filter);
        }
    }
}
