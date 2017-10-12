using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wc3Engine
{
    public class Compiler
    {
        public static string[] ReadVariablesFromScript(StreamReader script)
        {
            List<string> list = new List<string>();
            bool error = false;
            bool hasStruct = false;

            while (!script.EndOfStream)
            {
                string line = script.ReadLine();

                if (!hasStruct && line.Contains("struct "))
                    hasStruct = true;

                if (hasStruct)
                {
                    if (line.Contains("struct "))
                        error = true;

                    if (line.StartsWith("public "))
                    {
                        string type = GetTypeIfExist(line.After("public "));

                        if ("" != type)
                        {
                            //list.Add(CompilerTypes.List.Find());
                        }
                    }
                }
            }

            return null;
        }

        private static bool TypeExist(string textToParse)
        {
            foreach (string key in CompilerTypes.List)
            {
                if (textToParse.Contains(textToParse))
                    return true;
            }
            return false;
        }

        private static string GetTypeIfExist(string textToParse)
        {
            foreach (string key in CompilerTypes.List)
            {
                if (textToParse.Contains(textToParse))
                    return key;
            }
            return "";
        }
    }
}