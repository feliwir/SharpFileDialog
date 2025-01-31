using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{
    /// <summary>
    /// Represents a dialog for opening files.
    /// </summary>
    public class OpenFileDialog : IOpenFileDialogBackend
    {
        private readonly IOpenFileDialogBackend _backend;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialog"/> class.
        /// On Windows, the native dialog is used.
        /// On Linux, Zenity is used if available, otherwise Gtk is used.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
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

        /// <summary>
        /// Releases all resources used by the <see cref="OpenFileDialog"/>.
        /// </summary>
        public void Dispose()
        {
            _backend.Dispose();
        }

        /// <summary>
        /// Opens the file dialog.
        /// </summary>
        /// <param name="callback">The callback to be invoked when the dialog is closed.</param>
        /// <param name="filter">The filter for the types of files to be displayed.</param>
        public void Open(Action<DialogResult> callback, string filter = "All files(*.*) | *.*")
        {
            _backend.Open(callback, filter);
        }
    }
}
