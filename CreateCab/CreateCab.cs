using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;
using System;

namespace PSFExtractor.PackCab
{
    internal class CreateCab
    {
        private decimal progess;
        private  int left;
        private  int top;
        public decimal Prog { get => progess; set => progess = value; }
        private void ProgressHandler(object source, ArchiveProgressEventArgs e)
        {
            //CurrentFileNumber is zero-based.
            Prog = Decimal.Divide((decimal)e.CurrentFileNumber+1, (decimal)e.TotalFiles)*100;
            Console.SetCursorPosition(left, top);
            Console.CursorVisible = false;
            Console.Write("{0}%",(int) Prog);
        }


        public bool RunPackMethod(string cabFilePath, string sourceFolder)
        {
            try
            {
                CabInfo cabInfo = new CabInfo(cabFilePath);
                Console.Write("Creating full CAB... ");
                left = Console.CursorLeft;
                top = Console.CursorTop;
                cabInfo.Pack(sourceFolder, true, CompressionLevel.Max, ProgressHandler);
                Console.CursorVisible = true;
                Console.WriteLine();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
