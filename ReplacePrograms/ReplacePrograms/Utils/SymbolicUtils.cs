using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ReplacePrograms.Utils
{
    public static class SymbolicUtils
    {
        [DllImport("kernel32.dll", EntryPoint = "CreateSymbolicLinkW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CreateSymbolicLink([In] string lpSymlinkFileName, [In] string lpTargetFileName, [In] int dwFlags);

        [DllImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetFinalPathNameByHandle([In] IntPtr hFile, [Out] StringBuilder lpszFilePath, [In] int cchFilePath, [In] int dwFlags);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr SecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        private const int CREATION_DISPOSITION_OPEN_EXISTING = 3;
        private const int FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
        private const int SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1;

        /// <summary>
        /// Creates a new symbolic link.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="path"></param>
        /// <param name="replaceExisting"></param>
        public static void CreateSymbolicLink(string target, string path, bool replaceExisting)
        {
            if (replaceExisting) DeleteSymbolicLink(path);
            bool result = false;
            if (Directory.Exists(target))
            {
                result = CreateSymbolicLink(path, target, SYMBOLIC_LINK_FLAG_DIRECTORY);
            }
            else if (File.Exists(target))
            {
                result = CreateSymbolicLink(path, target, 0);
            }
            else
                throw new IOException("path not found");
            if (!result) throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Delete one symbolic link.
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteSymbolicLink(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path);
            if (File.Exists(path))
                File.Delete(path);
        }

        public static string GetRealPath(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path))
                throw new IOException("Path not found");

            DirectoryInfo symlink = new DirectoryInfo(path);//Es ist egel ob es eine Datei oder ein Ordner ist
            SafeFileHandle directoryHandle = CreateFile(symlink.FullName, 0, 2, (IntPtr)System.IntPtr.Zero, CREATION_DISPOSITION_OPEN_EXISTING, FILE_FLAG_BACKUP_SEMANTICS, System.IntPtr.Zero);//Handle zur Datei/Ordner
            if (directoryHandle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            StringBuilder result = new StringBuilder(512);
            int mResult = GetFinalPathNameByHandle(directoryHandle.DangerousGetHandle(), result, result.Capacity, 0);
            if (mResult < 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            if (result.Length >= 4 && result[0] == '\\' && result[1] == '\\' && result[2] == '?' && result[3] == '\\')
                return result.ToString().Substring(4);// "\\?\" entfernen
            else
                return result.ToString();
        }
    }
}