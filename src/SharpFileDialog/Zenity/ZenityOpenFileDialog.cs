using System;
using System.Diagnostics;

namespace SharpFileDialog.Zenity
{
    class ZenityOpenFileDialog : IOpenFileDialogBackend
    {
        string _title;
        Process _process;

        public ZenityOpenFileDialog(string title)
        {
            _title = title;
            _process = new Process();
            _process.StartInfo.FileName = "zenity";
            _process.StartInfo.Arguments = "--file-selection";
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            //_process.StartInfo.CreateNoWindow = true;

            if (title != null)
            {
                _process.StartInfo.Arguments += " --title \"" + title + "\"";
            }
        }

        public void Dispose()
        {
        }

        public void Open(string filter, Action<DialogResult> callback)
        {
            var filters = ZenityUtil.ConvertFilter(filter);
            foreach (var sFilter in filters)
            {
                _process.StartInfo.Arguments += " --file-filter=\"" + sFilter + "\"";
            }

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
