# PSFExtractor
Extract CAB+PSF update for Windows 10/11.

Usage: PSFExtractor.exe CABFILE

You need to put the CAB file alongside its corresponding PSF file. 
  
After that you'll get an installable CAB file. You can use it with dism /online /add-package.

The tool requires .NET Framework 4.8 and Windows 7 or above.

Supports any Windows 10/11 CU updates. (which can be downloaded with the UUPDump tool)
