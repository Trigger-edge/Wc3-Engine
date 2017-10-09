using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;

namespace Wc3Engine
{
    public class ShadowFlare
    {
        [DllImport("SFmpq.dll", EntryPoint = "SFMpqGetVersionString")]
        public static extern string DLLVersion();
        [DllImport("SFmpq.dll", EntryPoint = "SFileOpenArchive")]
        public static extern bool ArchiveOpen(string fileName, int mPQID, int p3, ref int hMPQ);
        [DllImport("SFmpq.dll", EntryPoint = "SFileCloseArchive")]
        public static extern bool ArchiveClose(int hMPQ);
        [DllImport("SFmpq.dll", EntryPoint = "MpqOpenArchiveForUpdate")]
        public static extern int OpenArchiveForUpdate(string lpFileName, int flags, int maximumFilesInArchive);
        [DllImport("SFmpq.dll", EntryPoint = "MpqAddFileToArchive")]
        public static extern bool AddFile(int hMPQ, string lpSourceFileName, string lpDestFileName, int flags);
        //[DllImport("SFmpq.dll", EntryPoint = "SFileListFiles")]
        //public static extern bool ListFiles(int hMPQ, string fileLists, ref string entry, int flags);
        [DllImport("SFmpq.dll", EntryPoint = "SFileOpenFile")]
        public static extern bool OpenFile(string fileName, ref int hFile);
        [DllImport("SFmpq.dll", EntryPoint = "SFileCloseFile")]
        public static extern bool CloseFile(int hFile);
        [DllImport("SFmpq.dll", EntryPoint = "SFileReadFile")]
        public static extern bool ReadFile(int hFile, byte[] buffer, uint numberOfBytesToRead, ref uint numberOfBytesRead, int overlapped);
        [DllImport("SFmpq.dll", EntryPoint = "SFileGetFileSize")]
        public static extern uint FileSize(int hFile, ref uint highPartOfFileSize);

    }

    public class MPQ
    {

        public class ArchiveFlag
        {
            internal const uint MAFA_EXISTS           = 0x80000000; //This flag will be added if not present
            internal const int  MAFA_UNKNOWN40000000  = 0x40000000; //Unknown flag
            internal const int  MAFA_SINGLEBLOCK      = 0x01000000; //File is stored as a single unit rather than being split by the block size
            internal const int  MAFA_MODCRYPTKEY      = 0x00020000; //Used with MAFA_ENCRYPT. Uses an encryption key based on file position and size
            internal const int  MAFA_ENCRYPT          = 0x00010000; //Encrypts the file. The file is still accessible when using this, so the use of this has depreciated
            internal const int  MAFA_COMPRESS         = 0x00000200; //File is to be compressed when added. This is used for most of the compression methods
            internal const int  MAFA_COMPRESS2        = 0x00000100; //File is compressed with standard compression only (was used in Diablo 1)
            internal const int  MAFA_REPLACE_EXISTING = 0x00000001; //If file already exists, it will be replaced
        }

        public class Archive
        {

            [DllImport("SFmpq.dll", EntryPoint = "MpqCompactArchive")]
            public static extern bool Compact(int hMPQ);

            public static int Open(string fileName)
            {
                int ret = -1;
                int result = -1;

                if (ShadowFlare.ArchiveOpen(fileName, 0, 0, ref result))
                    ret = result;

                return ret;
            }

            public static void Close(int hMPQ)
            {
                ShadowFlare.ArchiveClose(hMPQ);
            }

            public static List<string> ListFile(int hMPQ)
            {
                return File.ReadScript("(listfile)", hMPQ);
            }

            public static int OpenForUdate(string fileName)
            {
                return ShadowFlare.OpenArchiveForUpdate(fileName, 0x20, 8192);
            }

            public static bool AddFile(string source, string dest, int hMPQ)
            {
                return ShadowFlare.AddFile(hMPQ, source, dest, ArchiveFlag.MAFA_REPLACE_EXISTING + ArchiveFlag.MAFA_COMPRESS);
            }
        }

        public class File
        {
            
            public static byte[] Read(string fileName, int hMPQ)
            {
                int hFile = -1;
                if (ShadowFlare.OpenFile(fileName, ref hFile))
                {
                    uint fileSizeHigh = 0;
                    uint fileSize = ShadowFlare.FileSize(hFile, ref fileSizeHigh);
                    if ((fileSizeHigh == 0) && (fileSize > 0))
                    {
                        byte[] result = new byte[fileSize];
                        uint countRead = 0;
                        ShadowFlare.ReadFile(hFile, result, fileSize, ref countRead, 0);
                        ShadowFlare.CloseFile(hFile);
                        return result;
                    }
                }
                return null;
            }

            public static byte[] Read(string fileName, int hMPQ, bool externalFile)
            {
                if (externalFile)
                    return System.IO.File.ReadAllBytes(fileName);
                else
                    return Read(fileName, hMPQ);
            }

            public static List<string> ReadScript(string fileName, int hMPQ)
            {
                byte[] bytes = Read(fileName, hMPQ);

                if (null != bytes)
                {
                    StreamReader stream = new StreamReader(new MemoryStream(bytes));
                    List<string> list = new List<string>();
                    string line = stream.ReadLine();

                    while (line != null)
                    {
                        list.Add(line);
                        line = stream.ReadLine();
                    }

                    stream.Dispose();
                    return list;
                }
                return null;
            }

            public static List<string> ReadScript(byte[] bytes)
            {
                if (null != bytes)
                {
                    StreamReader stream = new StreamReader(new MemoryStream(bytes));
                    List<string> list = new List<string>();
                    string line = stream.ReadLine();

                    while (line != null)
                    {
                        list.Add(line);
                        line = stream.ReadLine();
                    }

                    stream.Dispose();
                    return list;
                }
                return null;
            }

            public static void WriteScript(List<string> script, string fileName, int hMPQ)
            {
                string tempPath = Settings.FOLDER_PATH + @"temp\";

                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                System.IO.File.Create(tempPath + fileName).Dispose();
                StreamWriter writer = new StreamWriter(tempPath + fileName);

                foreach (string line in script)
                    writer.WriteLine(line);

                writer.Dispose();

                Archive.AddFile(tempPath + fileName, fileName, hMPQ);
                System.IO.File.Delete(tempPath + fileName);
            }

            public static void Export(string fileName, string targetpath, int hMPQ)
            {
                if (!System.IO.File.Exists(targetpath))
                    System.IO.File.Create(targetpath).Dispose();

                System.IO.File.WriteAllBytes(targetpath, Read(fileName, hMPQ));
            }

            public static float Size(string fileName, int hMPQ)
            {
                uint fileSize = 0;
                int hFile = -1;
                if (ShadowFlare.OpenFile(fileName, ref hFile))
                {
                    uint fileSizeHigh = 0;
                    fileSize = ShadowFlare.FileSize(hFile, ref fileSizeHigh);
                }
                return Convert.ToSingle(fileSize) / 1000f;
            }

            public static string SizeAsString(string fileName, int hMPQ)
            {
                float result = Size(fileName, hMPQ);

                if (result < 1000f)
                    return Size(fileName, hMPQ).ToString() + " KB";
                else if (result >= 1000f)
                    return (Size(fileName, hMPQ) / 1000f).ToString().Substring(0, 5) + " MB";

                return "";
            }

            public static string GetType(string filename)
            {
                if (filename.ToLower().Contains(".mdx") || filename.ToLower().Contains(".mdl"))
                    return "Model";
                else if (filename.ToLower().Contains(".blp"))
                    return "Texture";
                else if (filename.ToLower().Contains(".j"))
                    return "Script";
                else if (filename.ToLower().Contains(".ai"))
                    return "AI Script";
                else if (filename.ToLower().Contains(".mp3") || filename.ToLower().Contains(".wav"))
                    return "Sound / Music";
                else if (filename.ToLower().Contains(".txt"))
                    return "Text";

                return "Other";
            }
        }
    }
}