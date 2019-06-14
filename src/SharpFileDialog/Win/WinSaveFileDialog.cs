using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog.Win
{
    class WinSaveFileDialog : ISaveFileDialogBackend
    {
        OpenFileName _openFileName;

        public WinSaveFileDialog(string title)
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

        public void Save(string filter, Action<DialogResult> callback)
        {
            _openFileName.filter = WinUtil.ConvertFilter(filter);
            bool success = WinInterop.GetSaveFileName(_openFileName);
            callback(new DialogResult()
            {
                FileName = _openFileName.file,
                Success = success
            });
        }
    }
}
