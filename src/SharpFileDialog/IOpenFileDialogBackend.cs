using System;

namespace SharpFileDialog
{
    internal interface IOpenFileDialogBackend : IDisposable
    {
        void Open(Action<DialogResult> callback, string filter);
    }
}
