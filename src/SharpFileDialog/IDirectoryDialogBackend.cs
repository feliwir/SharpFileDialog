using System;

namespace SharpFileDialog
{
    internal interface IDirectoryDialogBackend : IDisposable
    {
        void Open(Action<DialogResult> callback);
    }
}
