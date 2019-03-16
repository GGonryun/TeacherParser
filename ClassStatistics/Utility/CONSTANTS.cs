using System;
using System.IO;

namespace Utility
{
    public static class CONSTANTS
    {
        public static readonly string StartUpPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Utility", "HTML", "RawHTML.txt");

    }
}
