using System;

namespace SharpFileDialog
{
    internal interface IOpenFileDialogBackend : IDisposable
    {
        void Open(string filter, Action<bool> callback);
    }
}
