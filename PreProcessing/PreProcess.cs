using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;
using System;
using System.IO;

namespace PSFExtractor.PreProcessing
{
    internal class PreProcess
    {
        private static decimal progess;
        private static int left;
        private static int top;
        public static decimal Prog { get => progess; set => progess = value; }
        private static void ProgressHandler(object source, ArchiveProgressEventArgs e)
        {
            //CurrentFileNumber is zero-based - increment by 1 to enable proper calculation.
            Prog = Decimal.Divide((decimal)e.CurrentFileNumber + 1, (decimal)e.TotalFiles) * 100;
            Console.SetCursorPosition(left, top);
            Console.CursorVisible = false;
            Console.Write("{0}%", (int)Prog);
        }
        public static void Process(string CABFileName, string DirectoryName)
        {
            if (File.Exists(Path.Combine(DirectoryName, "express.psf.cix.xml")))
            {
                Console.WriteLine("CAB file is already expanded.");
            }
            else
            {
                CabInfo cabInfo = new CabInfo(CABFileName);
                Console.Write("Unpacking PSF CAB... ");
                left = Console.CursorLeft;
                top = Console.CursorTop;
                cabInfo.Unpack(DirectoryName, ProgressHandler);
                Console.CursorVisible = true;
                Console.WriteLine();
            }
        }
    }
}