using System;
using System.IO;

namespace ReplacePrograms.Utils
{
    public static class FolderUtils
    {
        /// <summary>
        /// Return the folder name from complete folder path.
        /// </summary>
        /// <param name="path">folder path included path name</param>
        /// <returns></returns>
        public static string GetFolderNameOfPath(string path)
        {
            string[] data = path.Split(new string[] { ":\\" }, StringSplitOptions.None);
            string[] second = data[1].Split(new string[] { "\\" }, StringSplitOptions.None);

            return second[second.Length - 1];
        }

        /// <summary>
        /// Move directory from source to destination folder
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void MoveDirectory(string source, string destination)
        {
            if (Directory.Exists(destination))
            {
                DeleteDirectory(destination);
            }

            Directory.Move(source, destination);
        }

        public static void CopyDirectory(string source, string destination)
        {
            // Create Rootfolder for destination.
            Directory.CreateDirectory(destination);

            // Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(source, destination));
            }

            // Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(source, destination), true);
            }
        }

        /// <summary>
        /// Remove folder if exists (thought for destination to check).
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}