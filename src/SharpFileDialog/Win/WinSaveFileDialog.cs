using System;
using System.Runtime.InteropServices;

namespace SharpFileDialog.Win
{
    class WinSaveFileDialog : ISaveFileDialogBackend
    {
        readonly OpenFileName _openFileName;
        public string DefaultFileName
        {
            set => _openFileName.file = value;
        }

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

        public void Save(Action<DialogResult> callback, string filter)
        {
            _openFileName.filter = WinUtil.ConvertFilter(filter);
            bool success = WinInterop.GetSaveFileName(_openFileName);
            callback(new DialogResult
            {
                FileName = _openFileName.file,
                Success = success
            });
        }
    }
}
