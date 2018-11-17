using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReplacePrograms.Models;

namespace ReplacePrograms.Utils
{
    class MigrationUtils
    {
        public static async Task Proceed(List<Migration> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                string SourceFolderName = FolderUtils.GetFolderNameOfPath(data[i].Source);
                string RootPath = FolderUtils.GetFullPathName(data[i].Source);
                string DestinationFolderName = data[i].Destination + "" + SourceFolderName;

                // Copy all stuff to new directory.
                FolderUtils.CopyDirectory(data[i].Source, DestinationFolderName);

                // Kills an process, if there are .exe files inside the rootfolders.
                KillProcess(data[i].Source);

                // Remove old folder to can create new Symbolic Links.
                FolderUtils.DeleteDirectory(data[i].Source);

                if (data[i].CreateSymbolicLink)
                {
                    // Create new symboliclink.
                    SymbolicLinkUtils.CreateSymbolicLinks(data[i].Source, DestinationFolderName, RootPath);
                    Console.WriteLine("symolic link created for {0} <<===>> {1}", data[i].Source, DestinationFolderName);
                }
            }

            await Task.Delay(1);
        }

        public static void KillProcess(string source)
        {
            string[] files = Directory.GetFiles(source);
            List<string> applications = new List<string>();    

            for (int j = 0; j < files.Length; j++)
            {
                if (files[j].Contains(".exe"))
                {
                    applications.Add(FolderUtils.GetFolderNameOfPath(files[j]));
                }
            }

            // Skip if we dont find anything inside the folder.
            if (applications.Count == 0)
                return;
            
            // Receive all processes that are excecuted.
            Process[] buffer = Process.GetProcesses();

            for(int i = 0; i < buffer.Length; i++)
            {
                for(int j = 0; j < applications.Count; j++)
                {
                    // If the name/process are equal we kill it, that we can delete the files save.
                    if (buffer[i].Equals(applications[j]))
                    {
                        buffer[i].Kill();
                    }
                }
            }
        }
    }
}