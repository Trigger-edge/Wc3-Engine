using System.Collections.Generic;

namespace Wc3Engine
{
    public class JassScript
    {
        internal static List<string> MainSystem = MPQ.File.ReadScript(Properties.Resources.MainSystem);
        public static List<string> InitList = new List<string>();

        public static void Load()
        {
            InitList.Add("Wc3Engine_MainSystem_OnInit");
        }
    }
}