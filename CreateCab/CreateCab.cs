using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;
using System;

namespace PSFExtractor.PackCab
{
    internal class CreateCab
    {
        public bool RunPackMethod(string cabFilePath, string sourceFolder)
        {
            try
            {
                CabInfo cabInfo = new CabInfo(cabFilePath);
                Console.Write("Creating full CAB... ");
                cabInfo.Pack(sourceFolder, true, (CompressionLevel)10, (EventHandler<ArchiveProgressEventArgs>)null);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, (object)"Packing cabinet failed.");
            }
            return false;
        }
    }
}
