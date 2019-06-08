using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{

    public class OpenFileDialog : IOpenFileDialogBackend
    {
        private IOpenFileDialogBackend _backend;

        public OpenFileDialog()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                _backend = new SharpFileDialog.GtkFileDialog();
            }

        }

        public void Dispose()
        {
            _backend.Dispose();
        }

        public void Open(string filter, Action<bool> callback)
        {
            _backend.Open(filter, callback);
        }
    }
}
