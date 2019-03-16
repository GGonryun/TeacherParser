using System;
using System.IO;

namespace Utility
{
    public static class CONSTANTS
    {
        public static readonly string RawHTMLPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Utility", "HTML", "RawHTML.txt");
        public static readonly string SdsuHTMLPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Utility", "HTML", "sdsuRawHTML.txt");

    }
}
