using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ReplacePrograms.Models.Enum;

namespace ReplacePrograms.Utils
{
    class MigrationUtils
    {
        public static void CreateSymbolicLinksAsync(string source, string destination)
        {
            string symbol = "/c mklink /j " + source + " " + destination + " & exit";

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";
            info.Arguments = symbol;

            Process process = new Process();
            process.StartInfo = info;
            process.Start();
        }

        public static void ProceedMigration(string source, string destination)
        {
            string SourceFolderName = FolderUtils.GetFolderNameOfPath(source);
            string DestinationFolderName = destination + "" + SourceFolderName;

            // Copy all stuff to new directory.
            FolderUtils.CopyDirectory(source, DestinationFolderName);

            // Remove old folder to can create new Symbolic Links.
            FolderUtils.DeleteDirectory(source);

            // Create new symbolic links from (source folder) to the new folder.
            // CreateSymbolicLinksAsync(source, DestinationFolderName);
            CreateSymbolicLinksAsync(source, DestinationFolderName);
            Console.WriteLine("symolic link created for {0} <<===>> {1}", source, DestinationFolderName);
        }
    }
}