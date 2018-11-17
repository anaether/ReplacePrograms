using System;
using System.IO;
using System.Diagnostics;
using IWshRuntimeLibrary;

namespace ReplacePrograms.Utils
{
    class MigrationUtils
    {
        public static void CreateSymbolicLinksAsync(string source, string destination, string rootpath)
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
            Console.WriteLine("Symbolic Link has been created.");
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

        public static void ProceedMigration(string source, string destination)
        {
            string SourceFolderName = FolderUtils.GetFolderNameOfPath(source);
            string RootPath = FolderUtils.GetFullPathName(source);
            string DestinationFolderName = destination + "" + SourceFolderName;

            // Copy all stuff to new directory.
            FolderUtils.CopyDirectory(source, DestinationFolderName);

            // Remove old folder to can create new Symbolic Links.
            FolderUtils.DeleteDirectory(source);

            // Create new symbolic links from (source folder) to the new folder.
            // CreateSymbolicLinksAsync(source, DestinationFolderName);
            //CreateSymbolicLinksAsync(source, DestinationFolderName, RootPath);

            //CreateShortcut(source, DestinationFolderName);
            CreateSymbolicLinksAsync(source, DestinationFolderName, RootPath);
            Console.WriteLine("symolic link created for {0} <<===>> {1}", source, DestinationFolderName);
        }
    }
}