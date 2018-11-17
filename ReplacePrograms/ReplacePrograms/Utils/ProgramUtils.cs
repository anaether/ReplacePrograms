using System;
using System.Security.Principal;

namespace ReplacePrograms.Utils
{
    public static class ProgramUtils
    {
        /// <summary>
        /// Check if program is started with admin rights.
        /// </summary>
        /// <returns></returns>
        public static bool HasAdministratorRights()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Restart Application when its not started with admin rights.
        /// </summary>
        public static void RestartApplication()
        {
            try
            {
                Environment.Exit(0);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Source);
            }
        }
    }
}