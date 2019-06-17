using System;

namespace SharpFileDialog
{
    interface ISaveFileDialogBackend : IDisposable
    {
        void Save(Action<DialogResult> callback, string filter);
    }
}
