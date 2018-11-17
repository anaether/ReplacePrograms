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
        /// Return the root folder from the source directory to can make: pushd (fixing symlincs with these method)
        /// </summary>
        /// <returns></returns>
        public static string GetFullPathName(string source)
        {
            string[] data = source.Split(new string[] { "\\" }, StringSplitOptions.None);
            string result = "";

            // data.Length-1 to skip the final folder name.
            for(int i = 0; i < data.Length - 1; i++)
            {
                if ((i + 1) == (data.Length - 2))
                    result += data[i];
                else
                    result += data[i] + "\\";
            }

            return result;
        }

        /// <summary>
        /// Move directory from source to destination folder
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void MoveDirectory(string source, string destination)
        {
            try
            {
                if (Directory.Exists(destination))
                {
                    DeleteDirectory(destination);
                }

                Directory.Move(source, destination);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Source);
            }
        }

        public static void CopyDirectory(string source, string destination)
        {
            try
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
            catch(Exception e)
            {
                Console.WriteLine(e.Source);
            }
        }

        /// <summary>
        /// Remove folder if exists (thought for destination to check).
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            try
            { 
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Source);
            }
        }
    }
}