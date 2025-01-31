using System;

namespace SharpFileDialog
{
    internal interface ISaveFileDialogBackend : IDisposable
    {
        string DefaultFileName { set; }
        void Save(Action<DialogResult> callback, string filter);
    }
}
