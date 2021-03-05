using System;
using UntieUnite.Core;

namespace UntieUnite
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: in_dir out_dir");
                Console.WriteLine(@"in_dir: full-path with downloaded assets (not apk), aka *\assets\DlcRoot\0.3.0\DLC_0");
                Console.WriteLine(@"out_dir: full-path to save all dumped data to.");
                return;
            }

            var inDir = args[0];
            var outDir = args[1];
            Dumper.ExtractBins(outDir, inDir);
        }
    }
}
