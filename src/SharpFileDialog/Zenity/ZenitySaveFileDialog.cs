using System;
using System.Diagnostics;

namespace SharpFileDialog.Zenity
{
    class ZenitySaveFileDialog : ISaveFileDialogBackend
    {
        string _title;
        Process _process;

        public ZenitySaveFileDialog(string title)
        {
            _title = title;
            _process = new Process();
            _process.StartInfo.FileName = "zenity";
            _process.StartInfo.Arguments = "--file-selection --save --confirm-overwrite";

            if (title != null)
            {
                _process.StartInfo.Arguments += " --title " + title;
            }
        }

        public void Dispose()
        {
        }

        public void Save(Action<DialogResult> callback)
        {
            _process.OutputDataReceived += (sender, data) =>
            {
                callback(new DialogResult()
                {
                    FileName = data.Data,
                    Success = true
                });
            };
            _process.Start();
        }
    }
}
