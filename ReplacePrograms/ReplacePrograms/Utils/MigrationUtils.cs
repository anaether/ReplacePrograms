using System;
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
    }
}