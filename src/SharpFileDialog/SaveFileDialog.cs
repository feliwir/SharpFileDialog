using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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
                _backend = new Gtk.GtkSaveFileDialog(title);
            }

        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Save(Action<DialogResult> callback)
        {
            _backend.Save(callback);
        }
    }
}
