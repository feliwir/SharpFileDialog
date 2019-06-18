using System;

namespace SharpFileDialog
{
    interface ISaveFileDialogBackend : IDisposable
    {
        string DefaultFileName { set; }
        void Save(Action<DialogResult> callback, string filter);
    }
}
