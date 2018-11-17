using System.IO;
using System.Diagnostics;
using IWshRuntimeLibrary;

namespace ReplacePrograms.Utils
{
    public static class SymbolicLinkUtils
    {
        public static void CreateSymbolicLinks(string source, string destination, string rootpath)
        {
            string symbol = "pushd " + rootpath + " & " + "/c mklink /D " + ((char)34) + source + ((char)34) + " " + ((char)34) + destination + ((char)34) + " & exit";

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";
            info.Arguments = symbol;

            Process process = new Process();
            process.StartInfo = info;
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Create Shortcut
        /// </summary>
        /// <param name="shortcutDir">Source directory where the shortcut will be saved.</param>
        /// <param name="targetPath">Target directory where the source for the shortcut is located.</param>
        public static void CreateShortcut(string shortcutDir, string targetPath)
        {
            string path = shortcutDir + ".lnk";
            WshShellClass shell = new WshShellClass();
            WshShortcut shortcut = (WshShortcut)shell.CreateShortcut(path);

            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
            shortcut.Arguments = "Argument";
            shortcut.Description = "This is a Shortcut";

            shortcut.Save();
        }
    }
}