using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static SharpFileDialog.Win.WinInterop;

namespace SharpFileDialog.Win
{
    // Based on https://stackoverflow.com/a/66187224
    internal class WinDirectoryDialog : IDirectoryDialogBackend
    {
        readonly List<string> _resultPaths = new List<string>();
        readonly List<string> _resultNames = new List<string>();

        public WinDirectoryDialog(string title)
        {
            Title = title;
        }

        public void Dispose() { }

        public void Open(Action<DialogResult> callback)
        {
            bool success = ShowDialog(IntPtr.Zero);
            callback(
                new DialogResult { FileName = _resultPaths.FirstOrDefault(), Success = success }
            );
        }

        public string InputPath { get; set; }
        public bool ForceFileSystem { get; set; }
        public bool Multiselect { get; set; }
        public string Title { get; set; }
        public string OkButtonLabel { get; set; }
        public string FileNameLabel { get; set; }

        int SetOptions(int options)
        {
            if (ForceFileSystem)
                options |= (int)FOS.FOS_FORCEFILESYSTEM;

            if (Multiselect)
                options |= (int)FOS.FOS_ALLOWMULTISELECT;

            return options;
        }

        bool ShowDialog(IntPtr owner, bool throwOnError = false)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var dialog = new FileOpenDialog() as IFileOpenDialog;
            if (dialog == null)
                return false;

            if (!string.IsNullOrEmpty(InputPath))
            {
                if (
                    CheckHr(
                        SHCreateItemFromParsingName(
                            InputPath,
                            null,
                            typeof(IShellItem).GUID,
                            out var item
                        ),
                        throwOnError
                    ) != 0
                )
                {
                    return false;
                }

                dialog.SetFolder(item);
            }

            var options = FOS.FOS_PICKFOLDERS;
            options = (FOS)SetOptions((int)options);
            dialog.SetOptions(options);

            if (Title != null)
                dialog.SetTitle(Title);

            if (OkButtonLabel != null)
                dialog.SetOkButtonLabel(OkButtonLabel);

            if (FileNameLabel != null)
                dialog.SetFileName(FileNameLabel);

            if (owner == IntPtr.Zero)
            {
                owner = Process.GetCurrentProcess().MainWindowHandle;
                if (owner == IntPtr.Zero)
                    owner = GetDesktopWindow();
            }

            var hr = dialog.Show(owner);
            if (hr == ERROR_CANCELLED)
                return false;

            if (CheckHr(hr, throwOnError) != 0)
                return false;

            if (CheckHr(dialog.GetResults(out var items), throwOnError) != 0)
                return false;

            items.GetCount(out var count);

            for (var i = 0; i < count; i++)
            {
                items.GetItemAt(i, out var item);
                CheckHr(
                    item.GetDisplayName(SIGDN.SIGDN_DESKTOPABSOLUTEPARSING, out var path),
                    throwOnError
                );

                CheckHr(
                    item.GetDisplayName(SIGDN.SIGDN_DESKTOPABSOLUTEEDITING, out var name),
                    throwOnError
                );

                if (path != null || name != null)
                {
                    _resultPaths.Add(path);
                    _resultNames.Add(name);
                }
            }

            return true;
        }

        static int CheckHr(int hr, bool throwOnError)
        {
            if (hr != 0 && throwOnError)
                Marshal.ThrowExceptionForHR(hr);
            return hr;
        }
    }
}
