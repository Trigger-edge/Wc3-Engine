using System.IO;
using System;
using System.Collections.Generic;

namespace Wc3Engine
{
    class WTS
    {

        internal const string FILE_NAME = "war3map.wts";

        private static List<string> file = new List<string>();
        public static int size = 0;


        public static void Read()
        {
            StreamReader stream = new StreamReader(new MemoryStream(MPQ.File.Read(FILE_NAME, Map.mpq)));

            file.Clear();

            string line = stream.ReadLine();

            while (line != null)
            {
                file.Add(line);

                if (line.StartsWith("STRING "))
                    size = Convert.ToInt32(line.Substring(7));

                line = stream.ReadLine();
            }

            stream.Dispose();
        }

        public static string Get(string key)
        {
            if (key.StartsWith("TRIGSTR_"))
            {
                string result = "";
                int count = 0;

                for (int i = IndexOf(key); file[i] != "}"; i++)
                {
                    if (0 == count)
                        result += file[i];
                    else
                        result += "|n" + file[i];
                    count++;
                }

                return result;
            }

            return key;
        }

        public static void Set(string key, string value)
        {
            file[IndexOf(key)] = value;
        }

        public static string New(string value)
        {
            size++;
            file.Add("STRING " + size);
            file.Add("{");
            file.Add(value);
            file.Add("}");
            file.Add("");

            string result = size.ToString();

            while (result.Length < 3)
                result = "0" + result;

            return "TRIGSTR_" + result;
        }

        private static int IndexOf(string key)
        {
            if (key.StartsWith("TRIGSTR_"))
                key = "STRING " + Convert.ToInt32(key.Substring(8)).ToString();

            if (key.StartsWith("STRING "))
            {
                int index = file.IndexOf(file.Find(x => x.Contains(key))) + 2;

                if (file[index].StartsWith("{"))
                    return index++;

                return index;
            }
            return 0;
        }
    }
}
