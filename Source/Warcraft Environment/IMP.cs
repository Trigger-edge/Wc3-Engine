using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wc3Engine
{
    class IMP
    {

        internal const string FILE_NAME = "war3map.imp";
        private const int BYTES_TO_SKIP = 9;

        //public static byte[] bytes;
        //private static List<string> list = new List<string>();

        public static int size = 0;


        public static void Read()
        {
            if (Map.listfile.Count() == 0)
                Wc3Engine.DebugMsg("Warning: no listfile detected!");
            else
            { 
                foreach (string item in Map.listfile)
                {
                    if (!Map.IsSytemFile(item))
                    {
                        size++;
                        Wc3Engine.This.assets_listView.Items.Add(new ListViewItem(new[] { size.ToString(), item, MPQ.File.GetType(item), MPQ.File.SizeAsString(item, Map.mpq) }));
                    }
                }
            }
        }
    }
}
