using System;
using System.Collections.Generic;
using System.IO;

namespace Wc3Engine
{
    public class JassFunction
    {
        public string functionName;
        private int _Count;
        public List<string> list;
        private static List<string> stack = new List<string>();

        public JassFunction(string funcName, string arguments)
        {
            if (!Exist(Jass.FuntionPreffix + funcName))
            {
                functionName = Jass.FuntionPreffix + funcName;
                list = new List<string>
                {
                    "",
                    "function " + functionName + " takes " + arguments + " returns nothing",
                    "endfunction"
                };
                stack.Add(functionName + " endfunction");
            }
        }

        public int Count
        {
            get { return _Count; }
        }

        public static bool Exist(string funcName)
        {
            return -1 != stack.IndexOf(Jass.FuntionPreffix + funcName + " endfunction");
        }

        public static int IndexOf(string funcName)
        {
            return stack.IndexOf(Jass.FuntionPreffix + funcName + " endfunction");
        }

        public void Remove()
        {
            stack.Remove(list.Find(x => x.Contains("function " + functionName)));
            list.TrimExcess();
            list = null;
            functionName = null;
        }

        public void InsertCall(int index, string code)
        {
            if (list.Exists(x => x.Contains("    call " + code)))
                list.Remove("    call " + code);
            else
                _Count++;

            if (index > list.Count)
                index = list.Count;
            else
                index = index + 2;

            list.Insert(index, "    call " + code);
        }
    }
    
    public class AbilityStruct
    {
        private string structName;
        private string suffix;
        private int _Count;
        public List<string> list;
        public JassFunction OnInit;
        public List<string> stackFunc;
        public List<JassFunction> functions;

        private static List<string> stack = new List<string>();

        public AbilityStruct(string abilityName, string abilitySuffix)
        {
            if (!Exist())
            {
                structName = abilityName;
                suffix = abilitySuffix;
                stack.Add("// Ability: " + structName);
                functions = new List<JassFunction>();
                stackFunc = new List<string>();

                list = new List<string>()
                {
                    "",
                    "// Ability: " + structName + " " + suffix,
                    "//===========================================================================",
                    "// struct " + structName + " begins",
                    "// struct " + structName + " ends",
                };

                OnInit = new JassFunction(structName + "_Init", "nothing");
                //OnInit.InsertCall(OnInit.Count, "Wc3Engine_System_DefineAbility()");
                Jass.Wc3EngineInit.InsertCall(Jass.Wc3EngineInit.Count, "ExecuteFunc(\"" + Jass.FuntionPreffix + structName + "_Init" + "\")");
            }
        }

        public int Count
        {
            get { return _Count; }
        }

        public bool Exist()
        {
            return stack.Exists(x => x.Contains("// Ability: " + structName + " " + suffix));
        }

        public bool FunctionExist(string funcName)
        {
            return -1 != stackFunc.IndexOf(Jass.FuntionPreffix + structName + funcName + " endfunction");
        }

        public int IndexOfFunction(string funcName)
        {
            return stackFunc.IndexOf(Jass.FuntionPreffix + structName + funcName + " endfunction");
        }

        public void Remove()
        {
            foreach (JassFunction func in functions)
                func.Remove();
            stack.Remove("// Ability: " + structName);
            stackFunc.TrimExcess();
            list.TrimExcess();
            functions.TrimExcess();
            stackFunc = null;
            list = null;
            functions = null;
            structName = null;
        }

        public JassFunction InsertFunction(int index, string funcName, string arguments)
        {
            if (FunctionExist(funcName))
            {
                int funcIndex = IndexOfFunction(funcName);
                functions[funcIndex].Remove();
                functions.Remove(functions[funcIndex]);
                stackFunc.RemoveAt(funcIndex);
            }
            else
                _Count++;

            if (index > functions.Count)
                index = functions.Count;

            stackFunc.Insert(index, Jass.FuntionPreffix + structName + funcName + " endfunction");
            functions.Insert(index, new JassFunction(structName + funcName, arguments));
            return functions[index];
        }
    }

    public class Jass
    {
        public static JassFunction Wc3EngineInit;
        public static List<string> script;

        public static string FuntionPreffix
        {
            get { return "Wc3Engine_"; }
        }

        public static string Identifier
        {
            get { return "//" + Wc3Engine.This.aboutBox.AssemblyProduct + " build code:"; }
        }

        public static string[] Header
        {
            get
            {
                return new string[] 
                {
                    "//===========================================================================",
                    "// Map name: " + WTS.Get(W3I.mapName),
                    "// Map Author: " + WTS.Get(W3I.mapAutor),
                    "// Date: " + DateTime.Now.ToString(),
                    "//==========================================================================="
                };
            }
        }

        public static void Read()
        {
            if (script != null)
            {
                script.TrimExcess();
                script = null;
            }
            script = MPQ.File.ReadScript("war3map.j",  Map.mpq);

            if (script.Exists(x => x.Contains(Identifier)))
            {
                int index = script.IndexOf(script.Find(x => x.Contains(Identifier)));
                script.RemoveRange(index - 1, script.Count - index + 1);
            }
            
            script.Add("");
            script.Add(Identifier);

            if (null != Wc3EngineInit)
                Wc3EngineInit.Remove();

            Wc3EngineInit = new JassFunction("Init", "nothing");

            foreach (string function in JassScript.InitList)
                Wc3EngineInit.InsertCall(Wc3EngineInit.Count, "ExecuteFunc(\"" + function + "\")");

            InsertCallToMainFunction("ExecuteFunc(\"" + FuntionPreffix + "Init\")");


            //MainSystemScript = MPQ.File.ReadScript(Properties.Resources.MainSystem);
            //Wc3Engine.DebugMsg(script.Count.ToString());
            //Wc3Engine.DebugMsg(MainSystemScript.Count.ToString());
        }

        public static void InsertCallToMainFunction(string code)
        {
            int index = script.IndexOf("function main takes nothing returns nothing");

            for (; !script[index].Contains("endfunction"); index++)
            {
                if (script[index].Contains(code))
                {
                    script.RemoveAt(index);
                    break;
                }
            }
            script.Insert(index, "    call " + code);
        }
    }
}
