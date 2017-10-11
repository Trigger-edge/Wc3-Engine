using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using BrightIdeasSoftware;

namespace Wc3Engine
{
    public partial class Wc3Engine : Form
    {

        internal static Version GameVersionBridge = Version.Parse("1.27.1");
        public static Version gameVersion;

        public static Wc3Engine This;
        internal AboutBox aboutBox  = new AboutBox();
        internal NameSuffixDialog nameSuffixDialog = new NameSuffixDialog();
        public Settings configBox = new Settings();

        public static TLVBase StandarAbilitiesTab;
        public static TLVBase CustomAbilitiesTab;


        public Wc3Engine()
        {
            This = this;
            InitializeComponent();
            PrintWc3Version();
            CreateAssetsListView();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            JassScript.Load();
            //GUIModule.Init();

            StandarAbilitiesTab = new TLVBase(LTVStadarAbilities, name_olvColumn2, false);
            CustomAbilitiesTab = new TLVBase(LTVCustomAbilities, name_olvColumn, true);
            Ability.TLVStandard = StandarAbilitiesTab;
            Ability.TLVCustom = CustomAbilitiesTab;

            //GUIHelper_accordion.Add(missileBasics_panel, "Missile Handle", "", 1, true);

            //DataVisualizer visualizer = new DataVisualizer(3);
            //guihelper_panel.Controls.Add(visualizer);
            //visualizer.Location = new Point(3, 590);


            mainTabControl.SelectedTab = mapInfoTab;
            abilities_tabControl.SelectedTab = custom_tabPage;

            if (Settings.LoadLastMap && Settings.LastMap != "")
                Map.Read(Settings.LastMap);
            else
                Map.SetPreview(Properties.Resources.Minimap_Unknown);

        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Map.Close();
        }

        public void PrintWc3Version()
        {
            if (Settings.GamePath1 != "")
                wc3Version.Text = "Warcraft III " + FileVersionInfo.GetVersionInfo(Settings.GamePath1 + "Game.dll").ProductVersion;
            else
                wc3Version.Text = "Warcraft III not detected";

            gameVersion = Version.Parse(string.Join("", Regex.Split(wc3Version.Text, @"[^0-9\.]+")).Substring(0, 6));
        }


        private void CreateAssetsListView()
        {
            // Width of -2 indicates auto-size.
            assets_listView.Columns.Add("Id", -2, HorizontalAlignment.Left);
            assets_listView.Columns.Add("File name", -2, HorizontalAlignment.Left);
            assets_listView.Columns.Add("Type", -2, HorizontalAlignment.Left);
            assets_listView.Columns.Add("Size", -2, HorizontalAlignment.Left);
        }

        //Show custom message box warning 
        public static void DebugMsg(string msg)
        {
            MessageBox.Show(msg, This.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
        }

        private void About_Click(object sender, EventArgs e)
        {
            aboutBox.ShowDialog();
        }

        private void MapTest_Click(object sender, EventArgs e)
        {
            Map.Save();
            WindowState = FormWindowState.Minimized;

            string gameExe = "war3.exe";
            string testPath = Settings.GamePath1 + @"Maps\Test\";
            
            if (gameVersion > GameVersionBridge)
            {
                gameExe = "Warcraft III.exe";
                testPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Warcraft III\Maps\Test\";
            }

            string testMap = testPath + "Wc3EngineTestMap" + Path.GetExtension(Settings.LastMap);

            if (!Directory.Exists(testPath))
                Directory.CreateDirectory(testPath);
            if (File.Exists(testMap))
                File.Delete(testMap);

            File.Copy(Settings.LastMap, testMap);

            Process process = Process.Start(Settings.GamePath1 + gameExe, "-window -loadfile \"" + testMap + "\"");
            process.WaitForExit();

            WindowState = FormWindowState.Normal;
        }

        
        /*
         Open Map
        */
        private void OpenMap_button_Click(object sender, EventArgs e)
        {
            if (openFile_map.ShowDialog() == DialogResult.OK)
            {
                Map.Read(openFile_map.FileName);
            }
        }

        private void ConfigButton_OnClick(object sender, EventArgs e)
        {
            configBox.ShowDialog();
        }

        private void EditMapInfo_OnClick(object sender, EventArgs e)
        {
            if (editMapInfo_button.Text == "Edit")
            {
                editMapInfo_button.Text = "Save";
                W3I.ShowEditBox(true);
            }

            else
            {
                W3I.Write(W3I.mapName, mapName_textBox.Text);
                W3I.Write(W3I.mapPlayers, mapPlayers_textBox.Text);
                W3I.Write(W3I.mapAutor, mapAutor_textBox.Text);
                W3I.Write(W3I.mapDescription, mapDescription_richTextBox.Text);

                editMapInfo_button.Text = "Edit";
                W3I.ShowEditBox(false);
            }
        }

        private void MapPreviewBox_Click(object sender, EventArgs e)
        {
            if (Map.opened && changeMapPreview_FileDialog.ShowDialog() == DialogResult.OK)
            {
                Image img = null;

                if (Path.GetExtension(changeMapPreview_FileDialog.FileName).ToLower() == ".tga")
                    img = Paloma.TargaImage.LoadTargaImage(changeMapPreview_FileDialog.FileName);
                else
                    img = Image.FromFile(changeMapPreview_FileDialog.FileName);
                
                if (img.Width != img.Height || (img.Width > Settings.MapPreviewWidth || img.Height > Settings.MapPreviewHeight))
                {
                    //
                }

                Map.SetPreview(img);
                img.Dispose();
            }
        }

        private void MinimapTab_Selected(object sender, TabControlEventArgs e)
        {
            
        }        

        private void OnNewAbilityClick(object sender, EventArgs e)
        {
            bool expand = false;
            string path = "";
            string name = "New Ability " + CustomAbilitiesTab.AbilityMasterList.
                FindAll(x => x.Path.Contains("New Ability ")).Count.ToString();

            if (null != LTVCustomAbilities.SelectedObject)
            {
                if (LTVCustomAbilities.SelectedObject is FolderModel)
                {
                    expand = true;
                    path = ((FolderModel)LTVCustomAbilities.SelectedObject).Path;
                }

                else if (LTVCustomAbilities.SelectedObject is AbilityModel && null != ((AbilityModel)LTVCustomAbilities.SelectedObject).Parent)
                    path = ((AbilityModel)LTVCustomAbilities.SelectedObject).Parent.Path;
            }

            Ability.Create(path + name, name);

            if (expand)
                LTVCustomAbilities.Expand(LTVCustomAbilities.SelectedObject);
        }

        private void OnNewFolderClick(object sender, EventArgs e)
        {
            bool expand = false;
            string path = "";

            if (null != LTVCustomAbilities.SelectedObject)
            {
                if (LTVCustomAbilities.SelectedObject is FolderModel)
                {
                    expand = true;
                    path = ((FolderModel)LTVCustomAbilities.SelectedObject).Path + @"\";
                }

                else if (LTVCustomAbilities.SelectedObject is AbilityModel && null != ((AbilityModel)LTVCustomAbilities.SelectedObject).Parent)
                    path = ((AbilityModel)LTVCustomAbilities.SelectedObject).Parent.Path + @"\";
            }

            //CustomAbilitiesTab.CreateFolder(path + "New Folder " + CustomAbilitiesTab.FolderMasterList.FindAll(x => x.Path.Contains("New Folder ")).Count.ToString());
            new FolderModel(CustomAbilitiesTab, path + "New Folder " + 
                CustomAbilitiesTab.FolderMasterList.FindAll(x => x.Path.Contains("New Folder ")).Count.ToString());

            if (expand)
                LTVCustomAbilities.Expand(LTVCustomAbilities.SelectedObject);
        }

        private void OnItemRemoveClick(object sender, EventArgs e)
        {
            if (LTVCustomAbilities.SelectedObject is FolderModel folder)
                CustomAbilitiesTab.DeleteObject(folder.Path);

            else if (LTVCustomAbilities.SelectedObject is AbilityModel _item)
                CustomAbilitiesTab.DeleteObject(_item.Path);

            LTVCustomAbilities.BuildList();
        }

        private void LTVAbility_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            /*object item = e.TargetModel;

            if (item == null)
                e.Effect = DragDropEffects.None;
            else if (item is FolderModel targetFolder)
                e.Effect = DragDropEffects.Move;
            else if (item is Item)
            {
                if (e.DropTargetLocation == DropTargetLocation.AboveItem || e.DropTargetLocation == DropTargetLocation.BelowItem)
                    e.Effect = DragDropEffects.Move;
                else
                {
                    e.Effect = DragDropEffects.None;
                    e.InfoMessage = "Can't drop this Ability here";
                }
            }*/
        }

        private void LTVAbility_ModelDropped(object sender, ModelDropEventArgs e)
        {
            /*// If they didn't drop on anything, then don't do anything
            if (e.TargetModel == null)
                return;

            if (e.TargetModel is FolderModel targetFolder)
            {
                foreach (object item in e.SourceModels)
                {
                    if (item is FolderModel sourceFolder)
                        CustomAbilitiesTab.Move(sourceFolder.Path, targetFolder.Path);

                    else if (item is Item sourceItem)
                        CustomAbilitiesTab.Move(sourceItem.Path, targetFolder.Path);
                }
            }

            e.RefreshObjects();*/
        }

        public Ability SelectedAbility;

        private void LTV_OnSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (null != e.Item && e.Item.Text.StartsWith("("))
            {
                string id = e.Item.Text.Remove(5, e.Item.Text.Length - 5).Substring(1);

                SelectedAbility = Ability.Find(id);
                SelectedAbility.UpdateOnSelect();
            }
        }

        private void LTV_OnCellEdit_DoubleClick(object sender, EventArgs e)
        {
            nameSuffixDialog.ShowDialog();
        }

        private void LTV_OnCellEdit_F2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) nameSuffixDialog.ShowDialog();
        }

        private void OnMissileNumericChanged(object sender, EventArgs e)
        {
            //GUIModule.UpdateMissileHandle();
        }
    }
}