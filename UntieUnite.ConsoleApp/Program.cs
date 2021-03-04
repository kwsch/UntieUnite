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
                return;
            }

            var inDir = args[0];
            var outDir = args[1];
            Dumper.ExtractBins(outDir, inDir);
        }
    }
}
