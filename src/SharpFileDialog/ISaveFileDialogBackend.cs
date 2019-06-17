using System;

namespace SharpFileDialog
{
    interface ISaveFileDialogBackend : IDisposable
    {
        string DefaultFileName { get; set; }
        void Save(Action<DialogResult> callback, string filter);
    }
}
