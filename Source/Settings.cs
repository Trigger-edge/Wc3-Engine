using System;
using System.IO;
using System.Windows.Forms;


namespace Wc3Engine
{
    public partial class Settings : Form
    {

        /*
          Constants
        */

        internal static string FOLDER_PATH     = Environment.ExpandEnvironmentVariables(@"%AppData%\TriggerEdge\");
        internal const string  FILE_NAME       = @"Settings.ini";

        internal const string INI_SECTION_GENERAL = "General";
        internal const string INI_SECTION_PATH    = "GamePath";

        /*
         End constants
        */

        internal static IniFile Ini = null;

        //Keys
        public static bool LoadLastMap = false;
        public static string LastMap   = "";

        public static int MapPreviewWidth  = 256;
        public static int MapPreviewHeight = 256;

        public static string GamePath1 = "";
        public static string GamePath2 = "";

        /*
         Init
        */

        public Settings()
        {
            InitializeComponent();
            
            Ini = new IniFile(FOLDER_PATH, FILE_NAME);

            //LoadLastMap
            if (!Ini.KeyExists(KeyName(new { LoadLastMap }), INI_SECTION_GENERAL))
                Ini.Write(KeyName(new { LoadLastMap }), Convert.ToString(Convert.ToInt16(LoadLastMap)), INI_SECTION_GENERAL);
            else
            {
                LastMap = Ini.Read(KeyName(new { LastMap }), INI_SECTION_GENERAL);
                LoadLastMap = Ini.Read(KeyName(new { LoadLastMap }), INI_SECTION_GENERAL) == "1";
                saveLastMap.Checked = LoadLastMap;

                if (saveLastMap.Checked)
                    saveLastMap.CheckState = CheckState.Checked;
            }


            //MapPreviewWidth
            if (!Ini.KeyExists(KeyName(new { MapPreviewWidth }), INI_SECTION_GENERAL))
                Ini.Write(KeyName(new { MapPreviewWidth }), MapPreviewWidth.ToString(), INI_SECTION_GENERAL);
            else
            {
                //Not implemented...
            }


            //MapPreviewHeight
            if (!Ini.KeyExists(KeyName(new { MapPreviewHeight }), INI_SECTION_GENERAL))
                Ini.Write(KeyName(new { MapPreviewHeight }), MapPreviewHeight.ToString(), INI_SECTION_GENERAL);
            else
            {
                //Not implemented...
            }


            //GamePath1
            if (!Ini.KeyExists(KeyName(new { GamePath1 }), INI_SECTION_PATH))
                Ini.Write(KeyName(new { GamePath1 }), GamePath1, INI_SECTION_PATH);
            else
            { 
                GamePath1 = Ini.Read(KeyName(new { GamePath1 }), INI_SECTION_PATH);
                gamePathTextBox1.Text = GamePath1;
            }


            //GamePath2
            if (!Ini.KeyExists(KeyName(new { GamePath2 }), INI_SECTION_PATH))
                Ini.Write(KeyName(new { GamePath2 }), GamePath2, INI_SECTION_PATH);
            else
            { 
                GamePath2 = Ini.Read(KeyName(new { GamePath2 }), INI_SECTION_PATH);
                gamePathTextBox2.Text = GamePath2;
            }
        }

        public static string KeyName<T>(T item) where T : class
        {
            if (item == null)
                return string.Empty;
            return typeof(T).GetProperties()[0].Name;
        }


        public static bool IsGamePathDefined()
        {
            return File.Exists(GamePath1 + "game.dll");
        }

        /*
         Event handler functions
        */

        private void checkBox_LoadLastMap_Changed(object sender, EventArgs e)
        {
            LoadLastMap = saveLastMap.Checked;
            Ini.Write(KeyName(new { LoadLastMap }), Convert.ToString(Convert.ToInt32(LoadLastMap)), INI_SECTION_GENERAL);
        }

        private void gamePathFileOpen1_Click(object sender, EventArgs e)
        {
            if (gamePathOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                GamePath1 = Path.GetDirectoryName(gamePathOpenFileDialog.FileName) + @"\";
                gamePathTextBox1.Text = GamePath1;
                Ini.Write(KeyName(new { GamePath1 }), GamePath1, INI_SECTION_PATH);
                Wc3Engine.This.PrintWc3Version();
            }
        }

        private void gamePathFileOpen2_Click(object sender, EventArgs e)
        {
            if (gamePathOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                GamePath2 = Path.GetDirectoryName(gamePathOpenFileDialog.FileName);
                gamePathTextBox2.Text = GamePath2;
                Ini.Write(KeyName(new { GamePath2 }), GamePath2, INI_SECTION_PATH);
            }
        }

        private void button_addCmd_Click(object sender, EventArgs e)
        {
            if (cmd_textBox.Text != "")
            { 
                cmd_listBox.Items.AddRange(new object[] { cmd_textBox.Text });
                cmd_textBox.Text = null;
            }
        }

        private void button_removeCmd_Click(object sender, EventArgs e)
        {
            cmd_listBox.Items.Clear();

            //MessageBox.Show(cmd_listBox.Items.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Close Settings Form
        private void settingsCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
