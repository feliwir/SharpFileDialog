﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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

        public void Save(Action<DialogResult> callback)
        {
            bool success = WinInterop.GetSaveFileName(_openFileName);
            callback(new DialogResult()
            {
                FileName = _openFileName.file,
                Success = success
            });
        }
    }
}