using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{
    /// <summary>
    /// Represents a dialog for selecting directories.
    /// </summary>
    public class DirectoryDialog : IDirectoryDialogBackend
    {
        private readonly IDirectoryDialogBackend _backend;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryDialog"/> class.
        /// On Windows, the native dialog is used (if a hang is experienced, ensure your Main method has an [STAThread] attribute).
        /// On Linux, Zenity is used if available, otherwise Gtk is used.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
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

        /// <summary>
        /// Releases all resources used by the <see cref="DirectoryDialog"/>.
        /// </summary>
        public void Dispose()
        {
            _backend.Dispose();
        }

        /// <summary>
        /// Opens the directory dialog.
        /// </summary>
        /// <param name="callback">The callback to be invoked when the dialog is closed.</param>
        public void Open(Action<DialogResult> callback)
        {
            _backend.Open(callback);
        }
    }
}
