using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace SharpFileDialog.Win
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal class OpenFileName
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;

        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;

        public String file = null;
        public int maxFile = 0;

        public String fileTitle = null;
        public int maxFileTitle = 0;

        public String initialDir = null;

        public String title = null;

        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;

        public String defExt = null;

        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;

        public String templateName = null;

        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    [ComImport, Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")] // CLSID_FileOpenDialog
    internal class FileOpenDialog { }

    [
        ComImport,
        Guid("d57c7288-d4ad-4768-be02-9d969532d960"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
    ]
    internal interface IFileOpenDialog
    {
        [PreserveSig]
        int Show(IntPtr parent); // IModalWindow

        [PreserveSig]
        int SetFileTypes(); // not fully defined

        [PreserveSig]
        int SetFileTypeIndex(int iFileType);

        [PreserveSig]
        int GetFileTypeIndex(out int piFileType);

        [PreserveSig]
        int Advise(); // not fully defined

        [PreserveSig]
        int Unadvise();

        [PreserveSig]
        int SetOptions(FOS fos);

        [PreserveSig]
        int GetOptions(out FOS pfos);

        [PreserveSig]
        int SetDefaultFolder(IShellItem psi);

        [PreserveSig]
        int SetFolder(IShellItem psi);

        [PreserveSig]
        int GetFolder(out IShellItem ppsi);

        [PreserveSig]
        int GetCurrentSelection(out IShellItem ppsi);

        [PreserveSig]
        int SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        [PreserveSig]
        int GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

        [PreserveSig]
        int SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

        [PreserveSig]
        int SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

        [PreserveSig]
        int SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [PreserveSig]
        int GetResult(out IShellItem ppsi);

        [PreserveSig]
        int AddPlace(IShellItem psi, int alignment);

        [PreserveSig]
        int SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        [PreserveSig]
        int Close(int hr);

        [PreserveSig]
        int SetClientGuid(); // not fully defined

        [PreserveSig]
        int ClearClientData();

        [PreserveSig]
        int SetFilter([MarshalAs(UnmanagedType.IUnknown)] object pFilter);

        [PreserveSig]
        int GetResults(out IShellItemArray ppenum);

        [PreserveSig]
        int GetSelectedItems([MarshalAs(UnmanagedType.IUnknown)] out object ppsai);
    }

    [
        ComImport,
        Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
    ]
    internal interface IShellItem
    {
        [PreserveSig]
        int BindToHandler(); // not fully defined

        [PreserveSig]
        int GetParent(); // not fully defined

        [PreserveSig]
        int GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        [PreserveSig]
        int GetAttributes(); // not fully defined

        [PreserveSig]
        int Compare(); // not fully defined
    }

    [
        ComImport,
        Guid("b63ea76d-1f85-456f-a19c-48159efa858b"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
    ]
    internal interface IShellItemArray
    {
        [PreserveSig]
        int BindToHandler(); // not fully defined

        [PreserveSig]
        int GetPropertyStore(); // not fully defined

        [PreserveSig]
        int GetPropertyDescriptionList(); // not fully defined

        [PreserveSig]
        int GetAttributes(); // not fully defined

        [PreserveSig]
        int GetCount(out int pdwNumItems);

        [PreserveSig]
        int GetItemAt(int dwIndex, out IShellItem ppsi);

        [PreserveSig]
        int EnumItems(); // not fully defined
    }

#pragma warning disable CA1712 // Do not prefix enum values with type name
    internal enum SIGDN : uint
    {
        SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000,
        SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000,
        SIGDN_FILESYSPATH = 0x80058000,
        SIGDN_NORMALDISPLAY = 0,
        SIGDN_PARENTRELATIVE = 0x80080001,
        SIGDN_PARENTRELATIVEEDITING = 0x80031001,
        SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
        SIGDN_PARENTRELATIVEPARSING = 0x80018001,
        SIGDN_URL = 0x80068000
    }

    [Flags]
    internal enum FOS
    {
        FOS_OVERWRITEPROMPT = 0x2,
        FOS_STRICTFILETYPES = 0x4,
        FOS_NOCHANGEDIR = 0x8,
        FOS_PICKFOLDERS = 0x20,
        FOS_FORCEFILESYSTEM = 0x40,
        FOS_ALLNONSTORAGEITEMS = 0x80,
        FOS_NOVALIDATE = 0x100,
        FOS_ALLOWMULTISELECT = 0x200,
        FOS_PATHMUSTEXIST = 0x800,
        FOS_FILEMUSTEXIST = 0x1000,
        FOS_CREATEPROMPT = 0x2000,
        FOS_SHAREAWARE = 0x4000,
        FOS_NOREADONLYRETURN = 0x8000,
        FOS_NOTESTFILECREATE = 0x10000,
        FOS_HIDEMRUPLACES = 0x20000,
        FOS_HIDEPINNEDPLACES = 0x40000,
        FOS_NODEREFERENCELINKS = 0x100000,
        FOS_OKBUTTONNEEDSINTERACTION = 0x200000,
        FOS_DONTADDTORECENT = 0x2000000,
        FOS_FORCESHOWHIDDEN = 0x10000000,
        FOS_DEFAULTNOMINIMODE = 0x20000000,
        FOS_FORCEPREVIEWPANEON = 0x40000000,
        FOS_SUPPORTSTREAMABLEITEMS = unchecked((int)0x80000000)
    }
#pragma warning restore CA1712 // Do not prefix enum values with type name

    internal class WinInterop
    {
#pragma warning disable IDE1006 // Naming Styles
        public const int ERROR_CANCELLED = unchecked((int)0x800704C7);
#pragma warning restore IDE1006 // Naming Styles

        [DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

        [DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);

        [DllImport("shell32")]
        public static extern int SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            IBindCtx pbc,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            out IShellItem ppv
        );

        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();
    }
}
