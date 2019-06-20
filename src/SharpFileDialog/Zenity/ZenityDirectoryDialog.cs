using System;
using System.Diagnostics;

namespace SharpFileDialog.Zenity
{
    class ZenityDirectoryDialog : IDirectoryDialogBackend
    {
        string _title;
        Process _process;

        public ZenityDirectoryDialog(string title)
        {
            _title = title;
            _process = new Process();
            _process.StartInfo.FileName = "zenity";
            _process.StartInfo.Arguments = "--file-selection --directory";
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;

            if (title != null)
            {
                _process.StartInfo.Arguments += " --title \"" + title + "\"";
            }
        }

        public void Dispose()
        {
        }

        public void Open(Action<DialogResult> callback)
        {
            _process.OutputDataReceived += (sender, data) =>
            {
                if (data.Data == null)
                {
                    return;
                }

                callback(new DialogResult()
                {
                    FileName = data.Data,
                    Success = true
                });
            };

            _process.Start();
            _process.BeginOutputReadLine();
        }
    }
}
