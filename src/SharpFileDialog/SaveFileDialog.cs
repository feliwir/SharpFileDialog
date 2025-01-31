using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog
{
    /// <summary>
    /// Represents a dialog for saving files.
    /// </summary>
    public class SaveFileDialog : ISaveFileDialogBackend
    {
        private readonly ISaveFileDialogBackend _backend;

        /// <summary>
        /// Gets or sets the default file name.
        /// </summary>
        public string DefaultFileName
        {
            set => _backend.DefaultFileName = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileDialog"/> class.
        /// On Windows, the native dialog is used.
        /// On Linux, Zenity is used if available, otherwise Gtk is used.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
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

        /// <summary>
        /// Releases all resources used by the <see cref="SaveFileDialog"/>.
        /// </summary>
        public void Dispose()
        {
            _backend.Dispose();
        }

        /// <summary>
        /// Opens the save file dialog.
        /// </summary>
        /// <param name="callback">The callback to be invoked when the dialog is closed.</param>
        /// <param name="filter">The filter for the types of files to be displayed.</param>
        public void Save(Action<DialogResult> callback, string filter = "All files(*.*) | *.*")
        {
            _backend.Save(callback, filter);
        }
    }
}
