using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplacePrograms.Utils
{
    public static class ProcessUtils
    {
        public static async Task EliminateProcesses(List<string> applications)
        {
            if (!ProgramUtils.HasAdministratorRights())
            {
                ProgramUtils.RestartApplication();
            }

            for(int i = 0; i < applications.Count; i++)
            {   

            }
        }
    }
}