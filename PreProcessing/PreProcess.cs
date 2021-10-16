using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;
using System;
using System.IO;

namespace PSFExtractor.PreProcessing
{
    internal class PreProcess
    {
        public static void Process(string CABFileName, string DirectoryName)
        {
            if (File.Exists(DirectoryName + Path.DirectorySeparatorChar.ToString() + "express.psf.cix.xml"))
            {
                Console.WriteLine("CAB file is already expanded.");
            }
            else
            {
                CabInfo cabInfo = new CabInfo(CABFileName);
                Console.Write("Unpacking PSF CAB... ");
                cabInfo.Unpack(DirectoryName);
                Console.WriteLine("OK");
            }
        }
    }
}