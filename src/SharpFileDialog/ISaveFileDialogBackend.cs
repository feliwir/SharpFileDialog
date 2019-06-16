using System;

namespace SharpFileDialog
{
    interface ISaveFileDialogBackend : IDisposable
    {
        void Save(string filter, Action<DialogResult> callback);
    }
}
