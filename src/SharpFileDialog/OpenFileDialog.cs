using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{

    public class OpenFileDialog : IOpenFileDialogBackend
    {
        private IOpenFileDialogBackend _backend;

        public OpenFileDialog(string title = null)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _backend = new Zenity.ZenityOpenFileDialog(title);

                //_backend = new Win.WinOpenFileDialog(title);
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                _backend = new Gtk.GtkOpenFileDialog(title);
            }
        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Open(string filter, Action<DialogResult> callback)
        {
            _backend.Open(filter, callback);
        }
    }
}
