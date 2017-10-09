using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Wc3Engine
{
    class W3I
    {

        internal const string FILE_NAME     = "war3map.w3i";
        private const int     BYTES_TO_SKIP = 12;
        
        public static byte[] bytes  = null;
        private static List<string> list = new List<string>();
        
        public static string mapName        = null;
        public static string mapPlayers     = null;
        public static string mapAutor       = null;
        public static string mapDescription = null;


        public static string RichTextBoxFormat()
        {
            return "|c00FEBA0EName:|r             "  + WTS.Get(mapName)    + "|n|n" +
                   "|c00FEBA0EPlayers:|r           " + WTS.Get(mapPlayers) + "|n|n" +
                   "|c00FEBA0EAutor:|r             " + WTS.Get(mapAutor)   + "|n|n" +
                   "|c00FEBA0EDescription:|r|n|n"    + WTS.Get(mapDescription);
        }


        byte[] header = new byte[] {
            0, // file format version = 18
            0, // number of saves (map version)
            0, // editor version (little endian)
            };

        public enum MapInfo : byte
        {
            /// <summary>
            /// No color map was included in the file.
            /// </summary>
            NO_COLOR_MAP = 0,

            /// <summary>
            /// Color map was included in the file.
            /// </summary>
            COLOR_MAP_INCLUDED = 1
        }


        public static void Read()
        {
            ShowLabel(true);

            bytes = MPQ.File.Read(FILE_NAME, Map.mpq);
            BinaryUtility.bytePosition = BYTES_TO_SKIP;
            
            mapName = BinaryUtility.GetSecuentialString(bytes, Encoding.ASCII, 1);
            mapAutor = BinaryUtility.GetSecuentialString(bytes, Encoding.ASCII, 1);
            mapDescription = BinaryUtility.GetSecuentialString(bytes, Encoding.ASCII, 1);
            mapPlayers = BinaryUtility.GetSecuentialString(bytes, Encoding.ASCII, 1);

            Tooltip.Print(Wc3Engine.This.mapDescription_richTextBox, RichTextBoxFormat(), true);
        }

        public static void Write(string key, string value)
        {
            //bytes = BinaryUtility.ReplaceTextInBytes(bytes, "TRIGSTR_001", "Hello???");

            if (mapName == key)
            {
                WTS.Set(mapName, value);
                //Wc3Color.RenderText(value, Launcher.Handle.mapName_pictureBox);
            }
            else if (mapPlayers == key)
            {
                WTS.Set(mapPlayers, value);
                //Wc3Color.RenderText(value, Launcher.Handle.mapPlayers_pictureBox);
            }
            else if (mapAutor == key)
            {
                WTS.Set(mapAutor, value);
                //Wc3Color.RenderText(value, Launcher.Handle.mapAutor_pictureBox);
            }
            else if (mapDescription == key)
            {
                WTS.Set(mapDescription, value);

                

            }

            Tooltip.Print(Wc3Engine.This.mapDescription_richTextBox, RichTextBoxFormat(), true);
        }
        /*
        private static string SecuentialRead()
        {
            List<byte> list = new List<byte>();
            byte currentByte = Buffer.GetByte(bytes, BYTES_TO_SKIP);

            while (currentByte != 0)
            {
                list.Add(currentByte);
                bytePosition++;
                currentByte = Buffer.GetByte(bytes, bytePosition);
            }

            bytePosition++;

            string result = Encoding.ASCII.GetString(list.ToArray());

            list.Clear();
            return result;
        }*/

        public static void ShowLabel(bool flag)
        {
            //Launcher.Handle.mapName_pictureBox.Visible = flag;
            //Launcher.Handle.mapAutor_pictureBox.Visible = flag;
            //Launcher.Handle.mapDescription_pictureBox.Visible = flag;
            //Launcher.Handle.mapPlayers_pictureBox.Visible = flag;
        }

        public static void ShowEditBox(bool flag)
        {
            ShowLabel(!flag);

            Wc3Engine.This.mapName_textBox.Text = WTS.Get(mapName);
            Wc3Engine.This.mapAutor_textBox.Text = WTS.Get(mapAutor);
            Wc3Engine.This.mapDescription_richTextBox.Text = WTS.Get(mapDescription);
            Wc3Engine.This.mapPlayers_textBox.Text = WTS.Get(mapPlayers);

            Wc3Engine.This.mapName_textBox.Visible = flag;
            Wc3Engine.This.mapAutor_textBox.Visible = flag;
            Wc3Engine.This.mapDescription_richTextBox.Visible = flag;
            Wc3Engine.This.mapPlayers_textBox.Visible = flag;
        }
    }
}
