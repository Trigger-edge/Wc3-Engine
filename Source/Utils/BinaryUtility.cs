using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3Engine
{
    class BinaryUtility
    {

        public static int bytePosition = 0;


        public static List<string> ListAllStrings(byte[] bytes, Encoding enc, int start, int end, int bytesToSkip)
        {
            if (null != bytes)
            {
                List<string> listStrings = new List<string>();
                List<byte> list = new List<byte>();
                
                for (int i = start; i < end || i < bytes.Length; i = i + bytesToSkip)
                {
                    byte currentByte = Buffer.GetByte(bytes, i);

                    if (currentByte > 0)
                    { 
                        while (currentByte != 0)
                        {
                            list.Add(currentByte);
                            i++;
                            currentByte = Buffer.GetByte(bytes, i);
                        }

                        listStrings.Add(enc.GetString(list.ToArray()));

                        list.Clear();
                    }
                }
                return listStrings;
            }
            return null;
        }

        public static string GetSecuentialString(byte[] bytes, Encoding enc, int bytesToSkip)
        {
            List<byte> list = new List<byte>();

            if (bytePosition <= Buffer.ByteLength(bytes))
            {
                byte currentByte = Buffer.GetByte(bytes, bytePosition);

                while (currentByte != 0)
                {
                    list.Add(currentByte);
                    bytePosition++;
                    currentByte = Buffer.GetByte(bytes, bytePosition);
                }

                bytePosition = bytePosition + bytesToSkip;

                string result = enc.GetString(list.ToArray());

                list.Clear();
                return result;
            }

            return null;
        }

        public static byte[] ReplaceTextInBytes(byte[] fileBytes, string oldText, string newText)
        {
            byte[] oldBytes = Encoding.ASCII.GetBytes(oldText),
                   newBytes = Encoding.ASCII.GetBytes(newText);

            int index = IndexOfBytes(fileBytes, oldBytes);

            if (index < 0)
            {
                // Text was not found
                return fileBytes;
            }

            byte[] newFileBytes = new byte[fileBytes.Length + newBytes.Length - oldBytes.Length];

            Buffer.BlockCopy(fileBytes, 0, newFileBytes, 0, index);
            Buffer.BlockCopy(newBytes, 0, newFileBytes, index, newBytes.Length);
            Buffer.BlockCopy(fileBytes, index + oldBytes.Length, newFileBytes, index + newBytes.Length, fileBytes.Length - index - oldBytes.Length);

            return newFileBytes;
        }

        private static int IndexOfBytes(byte[] searchBuffer, byte[] bytesToFind)
        {
            for (int i = 0; i < searchBuffer.Length - bytesToFind.Length; i++)
            {
                bool success = true;

                for (int j = 0; j < bytesToFind.Length; j++)
                {
                    if (searchBuffer[i + j] != bytesToFind[j])
                    {
                        success = false;
                        break;
                    }
                }

                if (success)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}