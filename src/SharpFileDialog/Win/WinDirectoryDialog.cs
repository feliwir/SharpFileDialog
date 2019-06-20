using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog.Win
{
    internal class WinDirectoryDialog : IDirectoryDialogBackend
    {
        OpenFileName _openFileName;

        public WinDirectoryDialog(string title)
        {
            _openFileName = new OpenFileName();
            _openFileName.structSize = Marshal.SizeOf(_openFileName);
            _openFileName.file = new string(new char[256]);
            _openFileName.maxFile = _openFileName.file.Length;
            _openFileName.title = title;
        }

        public void Dispose()
        {
        }

        public void Open(Action<DialogResult> callback)
        {
            // TODO: Implement the choosing a directory
            bool success = WinInterop.GetOpenFileName(_openFileName);
            callback(new DialogResult()
            {
                FileName = _openFileName.file,
                Success = success
            });

        }
    }
}