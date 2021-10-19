using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;
using System;
using System.IO;

namespace PSFExtractor.PreProcessing
{
    internal class PreProcess
    {
        private int left;
        private int top;
        public decimal Prog { get; set; }
        public int Left { get => left; set => left = value; }

        private void ProgressHandler(object source, ArchiveProgressEventArgs e)
        {
            //CurrentFileNumber is zero-based - increment by 1 to enable proper calculation.
            Prog = Decimal.Divide((decimal)e.CurrentFileNumber + 1, (decimal)e.TotalFiles) * 100;
            Console.SetCursorPosition(Left, top);
            Console.CursorVisible = false;
            Console.Write("{0}%", (int)Prog);
        }
        public void Process(string CABFileName, string DirectoryName)
        {
            if (File.Exists(Path.Combine(DirectoryName, "express.psf.cix.xml")))
            {
                Console.WriteLine("CAB file is already expanded.");
            }
            else
            {
                CabInfo cabInfo = new CabInfo(CABFileName);
                Console.Write("Unpacking PSF CAB... ");
                Left = Console.CursorLeft;
                top = Console.CursorTop;
                cabInfo.Unpack(DirectoryName, ProgressHandler);
                Console.CursorVisible = true;
                Console.WriteLine();
            }
        }
    }
}