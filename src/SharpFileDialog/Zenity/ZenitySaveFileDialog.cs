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

        public void Save(string filter, Action<DialogResult> callback)
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
