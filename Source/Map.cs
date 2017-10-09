using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Wc3Engine
{
    class Map
    {

        //Settings
        //internal const string TEMPDATA_FOLDER_PATH = @"%AppData%\TriggerEdge\tempdata\";
        internal const string WAR3_MAP_STRINGS = "war3map.wts";
        internal const string WAR3_PREVIEW = "war3mapPreview.tga";

        private const int TRANSPARENT_BORDER_WIDTH = 360;
        private const int TRANSPARENT_BORDER_HEIGHT = 360;


        private static Image preview = null;

        //public static string    workFolder = null;
        public static string file = null;
        public static List<string> listfile;// = new List<string>();
        public static int mpq = 0;
        public static bool opened = false;



        public static void Read(string filepath)
        {
            if (opened)
                Close();

            opened = true;
            file = filepath;
            mpq = MPQ.Archive.OpenForUdate(file);

            if (listfile != null)
            {
                listfile.TrimExcess();
                listfile = null;
            }
            listfile = MPQ.Archive.ListFile(mpq);

            Settings.Ini.Write(Settings.KeyName(new { Settings.LastMap }), file, Settings.INI_SECTION_GENERAL);
            Settings.LastMap = file;

            Jass.Read();
            WTS.Read();
            W3I.Read();
            IMP.Read();
            SLK.Read();

            byte[] mapPreview = MPQ.File.Read(WAR3_PREVIEW, mpq);
            if (null != mapPreview)
                SetPreview(Paloma.TargaImage.LoadTargaImage(new MemoryStream(mapPreview)));
        }

        public static void Close()
        {
            MPQ.Archive.Compact(mpq);
            MPQ.Archive.Close(mpq);
            W3I.ShowLabel(false);
            opened = false;
        }

        public static void Save()
        {
            
        }

        public static void SetPreview(Image img)
        {
            preview = img;

            Wc3Engine.This.mapBorder_pictureBox.Image.Dispose();
            Wc3Engine.This.mapBorder_pictureBox.Image = new Bitmap(TRANSPARENT_BORDER_WIDTH, TRANSPARENT_BORDER_HEIGHT);

            Graphics graphics = Graphics.FromImage(Wc3Engine.This.mapBorder_pictureBox.Image);

            Rectangle cover = new Rectangle(0, 0, TRANSPARENT_BORDER_WIDTH, TRANSPARENT_BORDER_HEIGHT);
            Rectangle minimap = new Rectangle(50, 53, 260, 260);

            graphics.DrawImage(img, minimap);
            graphics.DrawImage(Properties.Resources.MinimapCover, cover);

            graphics.Dispose();
        }

        public static bool IsSytemFile(string filename)
        {
            string[] list = new[] {
                            "war3map.w3b",
                            "war3map.w3e",
                            "war3map.w3i",
                            "war3map.wtg",
                            "war3map.wct",
                            "war3map.wts",
                            "war3map.j",
                            "war3map.shd",
                            "war3mapmap.blp",
                            "war3mapmap.blp",
                            "war3mappreview.tga",
                            "war3map.mmp",
                            "war3map.wpm",
                            "war3map.doo",
                            "war3mapunits.doo",
                            "war3map.w3s",
                            "war3map.w3r",
                            "war3map.w3c",
                            "war3map.w3u",
                            "war3map.w3t",
                            "war3map.w3d",
                            "war3map.w3a",
                            "war3map.w3h",
                            "war3map.imp",
                            "war3mapmisc.txt",
                            "war3mapskin.txt", };

            foreach (string item in list)
            {
                if (item == filename.ToLower())
                    return true;
            }

            return false;
        }
    }
}
