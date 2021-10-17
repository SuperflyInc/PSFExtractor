using PSFExtractor.PackCab;
using System;
using System.Collections;
using System.IO;

namespace PSFExtractor
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("PSFExtractor by th1r5bvn23, abbodi1406 - v1.0.8.1 modified by Superfly\nVisit https://www.betaworld.cn/\n");
            if (args.Length != 1)
            {
                PrintHelp();
                return 0;
            }
            string[] strArray = args[0].Split('_');
            if (strArray.Length > 1)
                    RenameFiles();
            string CABFileName = strArray.Length > 1 ? string.Concat(strArray[0] , ".cab") : args[0];
            string PSFFileName = CABFileName.Replace(".cab", ".psf");

            if (!File.Exists(CABFileName))
            {
                PrintError(1);
                return 1;
            }
            string directoryRoot = Directory.GetDirectoryRoot(CABFileName);
            string DirectoryName = Path.Combine(directoryRoot, "Extracted", Path.GetFileNameWithoutExtension(CABFileName));
            try
            {
                Directory.CreateDirectory(DirectoryName);
            }
            catch(Exception)
            {
                PrintError(2);
                return 1;
            }

            if (!File.Exists(PSFFileName))
            {
                PrintError(3);
                return 1;
            }
            try
            {
                PreProcessing.PreProcess.Process(CABFileName, DirectoryName);
            }
            catch(Exception)
            {
                PrintError(4);
                return 1;
            }
            SplitPSF.DeltaFileList.List = new ArrayList();
            try
            {
                SplitPSF.GenerateFileList.Generate(PSFFileName, DirectoryName);
            }
            catch(Exception)
            {
                PrintError(5);
                return 1;
            }
            try
            {
                SplitPSF.SplitOutput.WriteOutput(PSFFileName, DirectoryName);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                PrintError(6);
                return 1;
            }
            try
            {
                CreateCab createCab = new CreateCab();
                string withoutExtension = Path.GetFileNameWithoutExtension(CABFileName);
                string path1 = Directory.CreateDirectory(CABFileName.Substring(0, CABFileName.LastIndexOf('.'))).ToString();
                createCab.RunPackMethod(Path.Combine(path1, withoutExtension + ".cab"), DirectoryName);
                if (Directory.Exists(Path.Combine(directoryRoot, "Extracted")))
                    Directory.Delete(Path.Combine(directoryRoot, "Extracted"), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Program.PrintError(7);
                return 1;
            }

            PrintError(0);
            return 0;
        }

        static void PrintHelp()
        {
            //                |---------------------------------------80---------------------------------------|
            // Won't implement these features until v1.1
            /*
            Console.WriteLine("Usage: PSFExtractor.exe <CAB file>\n" +
                              "       PSFExtractor.exe --manual <PSF file> <XML file> [--prefer <type>]\n\n" +
                              "    Auto mode      Auto detect CAB file and PSF file. Works only for Windows 10+.\n" +
                              "    Manual mode    Specify PSF file and its descriptive XML file manually. Works\n" +
                              "                   for any update.\n" +
                              "    <CAB file>     Path to CAB file. Use only in auto mode. Need corresponding\n" +
                              "                   PSF file with the same name in the same location.\n" +
                              "    <PSF file>     Path to PSF file. Use only in manual mode.\n" +
                              "    <XML file>     Path to XML file. Use only in manual mode.\n" +
                              "    <type>         Specify the preferred type when multiple source is available.\n" +
                              "                   Default value is RAW.\n");
            */
            Console.WriteLine("Usage: PSFExtractor.exe <CAB file>\n");
        }
        private static void RenameFiles()
        {
            //UUP dump file names have a partial checksum suffix - remove that.
            foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                if (Path.GetExtension(file) == ".cab" || Path.GetExtension(file) == ".psf")
                {
                    string[] strArray = Path.GetFileNameWithoutExtension(file).Split('_');
                    string destFileName = file.Replace("_" + strArray[1], string.Empty);
                    File.Move(file, destFileName);
                }
            }
        }

        static void PrintError(int ErrorCode)
        {
            switch (ErrorCode)
            {
                case 0:
                    Console.WriteLine("Finished!");
                    break;
                case 1:
                    Console.WriteLine("Error: no CAB file.");
                    break;
                case 2:
                    Console.WriteLine("Error: failed to create directory.");
                    break;
                case 3:
                    Console.WriteLine("Error: no corresponding PSF file.");
                    break;
                case 4:
                    Console.WriteLine("Error: expand failed.");
                    break;
                case 5:
                    Console.WriteLine("Error: invalid CAB file.");
                    break;
                case 6:
                    Console.WriteLine("Error: failed to write output file.");
                    break;
            }
        }
    }
}
