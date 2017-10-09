using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Wc3Engine
{

    #region AbilityData.slk class fields
    internal class AbilityData
    {
        public struct Field
        { 
            internal const int alias        = 1;
            internal const int code         = 2;
            internal const int comments     = 3;
            internal const int version      = 4;
            internal const int useInEditor  = 5;
            internal const int hero         = 6;
            internal const int item         = 7;
            internal const int sort         = 8;
            internal const int race         = 9;
            internal const int checkDep     = 10;
            internal const int levels       = 11;
            internal const int reqLevel     = 12;
            internal const int levelSkip    = 13;
            internal const int priority     = 14;
            internal const int targs1       = 15;
            internal const int Cast1        = 16;
            internal const int Dur1         = 17;
            internal const int HeroDur1     = 18;
            internal const int Cool1        = 19;
            internal const int Cost1        = 20;
            internal const int Area1        = 21;
            internal const int Rng1         = 22;
            internal const int DataA1       = 23;
            internal const int DataB1       = 24;
            internal const int DataC1       = 25;
            internal const int DataD1       = 26;
            internal const int DataE1       = 27;
            internal const int DataF1       = 28;
            internal const int DataG1       = 29;
            internal const int DataH1       = 30;
            internal const int DataI1       = 31;
            internal const int UnitID1      = 32;
            internal const int BuffID1      = 33;
            internal const int EfctID1      = 34;
            internal const int targs2       = 35;
            internal const int Cast2        = 36;
            internal const int Dur2         = 37;
            internal const int HeroDur2     = 38;
            internal const int Cool2        = 39;
            internal const int Cost2        = 40;
            internal const int Area2        = 41;
            internal const int Rng2         = 42;
            internal const int DataA2       = 43;
            internal const int DataB2       = 44;
            internal const int DataC2       = 45;
            internal const int DataD2       = 46;
            internal const int DataE2       = 47;
            internal const int DataF2       = 48;
            internal const int DataG2       = 49;
            internal const int DataH2       = 50;
            internal const int DataI2       = 51;
            internal const int UnitID2      = 52;
            internal const int BuffID2      = 53;
            internal const int EfctID2      = 54;
            internal const int targs3       = 55;
            internal const int Cast3        = 56;
            internal const int Dur3         = 57;
            internal const int HeroDur3     = 58;
            internal const int Cool3        = 59;
            internal const int Cost3        = 60;
            internal const int Area3        = 61;
            internal const int Rng3         = 62;
            internal const int DataA3       = 63;
            internal const int DataB3       = 64;
            internal const int DataC3       = 65;
            internal const int DataD3       = 66;
            internal const int DataE3       = 67;
            internal const int DataF3       = 68;
            internal const int DataG3       = 69;
            internal const int DataH3       = 70;
            internal const int DataI3       = 71;
            internal const int UnitID3      = 72;
            internal const int BuffID3      = 73;
            internal const int EfctID3      = 74;
            internal const int targs4       = 75;
            internal const int Cast4        = 76;
            internal const int Dur4         = 77;
            internal const int HeroDur4     = 78;
            internal const int Cool4        = 79;
            internal const int Cost4        = 80;
            internal const int Area4        = 81;
            internal const int Rng4         = 82;
            internal const int DataA4       = 83;
            internal const int DataB4       = 84;
            internal const int DataC4       = 85;
            internal const int DataD4       = 86;
            internal const int DataE4       = 87;
            internal const int DataF4       = 88;
            internal const int DataG4       = 89;
            internal const int DataH4       = 90;
            internal const int DataI4       = 91;
            internal const int UnitID4      = 92;
            internal const int BuffID4      = 93;
            internal const int EfctID4      = 94;
            internal const int InBeta       = 95;
        }

        private static List<string> codeId = new List<string>();
        private static List<int> id = new List<int>();

        public static List<string> StringList = new List<string>();
        public static List<string> fncList = new List<string>();

        public static string[,] table;
        public static int sizeX = 0;
        public static int sizeY = 0;


        private static void ReadFileTypes()
        {
            SLK.LoadList(ref SLK.list, "AbilityData.slk");

            /*File.Create(Settings.FOLDER_PATH + "AbilityData.slk").Dispose();
            StreamWriter writer = new StreamWriter(Settings.FOLDER_PATH + "AbilityData.slk");

            foreach (string line in SLK.list)
                writer.WriteLine(line);

            writer.Dispose();*/


            //Abilities strings
            SLK.LoadList(ref StringList, "CampaignAbilityStrings.txt");
            SLK.LoadList(ref StringList, "CommonAbilityStrings.txt");
            SLK.LoadList(ref StringList, "HumanAbilityStrings.txt");
            SLK.LoadList(ref StringList, "ItemAbilityStrings.txt");
            SLK.LoadList(ref StringList, "NeutralAbilityStrings.txt");
            SLK.LoadList(ref StringList, "NightElfAbilityStrings.txt");
            SLK.LoadList(ref StringList, "OrcAbilityStrings.txt");
            SLK.LoadList(ref StringList, "UndeadAbilityStrings.txt");

            //Abilities func
            SLK.LoadList(ref fncList, "CampaignAbilityFunc.txt");
            SLK.LoadList(ref fncList, "CommonAbilityFunc.txt");
            SLK.LoadList(ref fncList, "HumanAbilityFunc.txt");
            SLK.LoadList(ref fncList, "ItemAbilityFunc.txt");
            SLK.LoadList(ref fncList, "NeutralAbilityFunc.txt");
            SLK.LoadList(ref fncList, "NightElfAbilityFunc.txt");
            SLK.LoadList(ref fncList, "OrcAbilityFunc.txt");
            SLK.LoadList(ref fncList, "UndeadAbilityFunc.txt");
        }

        public static void Read()
        {
            ReadFileTypes();

            table = new string[SLK.GetSizeX(SLK.list) + 1, SLK.GetSizeY(SLK.list) + 1];
            sizeY = 0;

            foreach (string line in SLK.list)
            {
                sizeX = SLK.GetLineX(line);

                if (0 != SLK.GetLineY(line))
                    sizeY = SLK.GetLineY(line);

                if (line.StartsWith("C;") && sizeY > 1)
                {
                    table[sizeX, sizeY] = line.After(";K");

                    if (sizeX == Field.InBeta)
                    {
                        codeId.Add(table[sizeX, sizeY]);
                        id.Add(sizeY);

                        string folderPath = table[Field.race, sizeY].Replace("\"", null).Substring(0, 1).ToUpper() + table[Field.race, sizeY].Replace("\"", null).Substring(1);
                        string subFolder = "Units";

                        if (table[Field.hero, sizeY] == "1" && table[Field.item, sizeY] == "0")
                            subFolder = "Heroes";
                        else if (table[Field.hero, sizeY] == "0" && table[Field.item, sizeY] == "1")
                            subFolder = "Items";

                        folderPath = folderPath + @"\" + subFolder;

                        if (!Wc3Engine.StandarAbilitiesTab.FolderExist(folderPath))
                            Wc3Engine.StandarAbilitiesTab.CreateFolder(folderPath);

                        string objectId = table[Field.code, sizeY].Replace("\"", null);

                        Wc3Engine.StandarAbilitiesTab.CreateItem(folderPath + @"\" + DataField.GetValue(objectId, DataField.Name, StringList), objectId.ToIntObjectId());
                    }
                }
            }

            SLK.list.Clear();
        }

        public static string ReadString(int x, int y)
        {
            return table[x, y];
        }

        public static int ReadInt(int x, int y)
        {
            if (!table[x, y].Contains("\""))
                return Convert.ToInt32(table[x, y]);
            else
                return 0;
        }

        public static decimal ReadDecimal(int x, int y)
        {
            if (!table[x, y].Contains("\""))
                return Convert.ToDecimal(table[x, y]);
            else
                return 0;
        }

        public static void SetValue(string value, int x, int y)
        {
            table[x, y] = value;
        }

        public static void SetValue(int value, int x, int y)
        {
            table[x, y] = value.ToString();
        }

        public static void SetValue(decimal value, int x, int y)
        {
            table[x, y] = value.ToString();
        }
    }
    #endregion

    internal class DataField
    {
        internal const string Name            = "Name=";
        internal const string EditorSuffix    = "EditorSuffix=";
        internal const string Tip             = "Tip=";
        internal const string Untip           = "Untip=";
        internal const string Ubertip         = "Ubertip=";
        internal const string Requires        = "Requires=";
        internal const string Requiresamount  = "Requiresamount=";
        internal const string Researchtip     = "Researchtip=";
        internal const string ResearchUberTip = "ResearchUberTip=";
        internal const string Hotkey          = "Hotkey=";
        internal const string Researchhotkey  = "Researchhotkey=";
        internal const string Unhotkeyme      = "Unhotkey=";

        internal const string Order    = "Order=";
        internal const string Unorder  = "Unorder=";
        internal const string Orderon  = "Orderon=";
        internal const string Orderoff = "Orderoff=";

        internal const string Art          = "Art=";
        internal const string Unart        = "Unart=";
        internal const string Missileart   = "Missileart=";
        internal const string Missilespeed = "Missilespeed=";
        internal const string Animnames    = "Animnames=";
        internal const string Specialart   = "Specialart=";
        internal const string Effectart    = "Effectart=";
        internal const string Casterart    = "Casterart=";
        internal const string Targetart    = "Targetart=";

        internal const string Buttonpos         = "Buttonpos=";
        internal const string Researchbuttonpos = "Researchbuttonpos=";

        internal const string Effectsound   = "Effectsound=";
        internal const string MissileHoming = "MissileHoming=";
        

        public static string GetValue(string objectId, string objectField, List<string> list)
        {
            if (list.Contains("[" + objectId + "]"))
            {
                for (int i = list.IndexOf("[" + objectId + "]") + 1; i < list.Count && !list[i].StartsWith("["); i++)
                {
                    if (list[i].Contains(objectField))
                        return list[i].Replace(objectField, null);
                }
            }

            return objectId;
        }

        /*public static void SetValue(string value, string objectId, string objectField, List<string> list)
        {
            string search = list.Find(x => x.Contains("[" + objectId + "]"));

            if (list.Contains(search))
            {
                for (int i = list.IndexOf(search); i < list.Count; i++)
                {
                    if (list[i].Contains(objectField))
                        list.Insert(i, objectField + value);

                    if ("" == list[i])
                        break;
                }
            }
        }*/
    }

    class SLK
    {

        public static List<string> list = new List<string>();
        public static int MpqData;


        public static void Read()
        {
            if (Settings.IsGamePathDefined())
            {
                LoadMpqData();
                AbilityData.Read();
                MPQ.Archive.Close(MpqData);
            }
        }

        public static void LoadMpqData()
        {
            if (Wc3Engine.gameVersion > Wc3Engine.GameVersionBridge)
                MpqData = MPQ.Archive.Open(Settings.GamePath1 + "War3xlocal.mpq");

            else if (Wc3Engine.gameVersion <= Wc3Engine.GameVersionBridge)
                MpqData = MPQ.Archive.Open(Settings.GamePath1 + "War3Patch.mpq");
        }

        internal static void LoadList(ref List<string> localList, string file)
        {
            StreamReader stream = new StreamReader(new MemoryStream(MPQ.File.Read(@"Units\" + file, MpqData)));
            string line = stream.ReadLine();

            while (line != null)
            {
                if (!line.StartsWith("//") && "" != line)
                {
                    if (6 == line.Length && line.StartsWith("[") && line.EndsWith("]") && localList.Contains(line) && AbilityData.StringList == localList)
                    {
                        int index = localList.IndexOf(line) + 1;
                        line = stream.ReadLine();

                        while ("" != line)
                        {
                            localList.Insert(index, line);
                            line = stream.ReadLine();
                        }
                    }
                    else
                        localList.Add(line);
                }

                line = stream.ReadLine();
            }

            stream.Dispose();
        }

        internal static int GetSizeX(List<string> file)
        {
            if (file[1].ToString().StartsWith("B;X"))
                return Convert.ToInt32(SubstringExtensions.Between(file[1].ToString(), "X", ";Y"));
            else
                return 0;
        }

        internal static int GetSizeY(List<string> file)
        {
            if (file[1].ToString().StartsWith("B;X"))
                return Convert.ToInt32(SubstringExtensions.Between(file[1].ToString(), "Y", ";D0"));
            else
                return 0;
        }

        internal static int GetLineX(string line)
        {
            if (line.StartsWith("C;X") && line.Contains(";Y"))
                return Convert.ToInt32(SubstringExtensions.Between(line, "C;X", ";Y"));
            else if (line.StartsWith("C;X") && line.Contains(";K"))
                return Convert.ToInt32(SubstringExtensions.Between(line, "C;X", ";K"));
            else
                return 0;
        }

        internal static int GetLineY(string line)
        {
            if (line.StartsWith("C;X") && line.Contains(";Y"))
                return Convert.ToInt32(SubstringExtensions.Between(line, ";Y", ";K"));
            else
                return 0;
        }
    }
}
