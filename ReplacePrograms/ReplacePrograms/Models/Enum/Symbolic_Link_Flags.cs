using System;

namespace ReplacePrograms.Models.Enum
{
    [Flags]
    public enum SYMBOLIC_LINK_FLAG
    {
        File = 0,
        Directory = 1,
        AllowUnprivilegedCreate = 2
    }
}