using System;
using System.Linq;
using System.Windows.Forms;

namespace Wc3Engine
{
    public class GUIModule
    {
        internal const int MAX_ABILITY_LEVEL = 100;

        //private Wc3Engine Handle;
        private static int[,] lvl = new int[8, MAX_ABILITY_LEVEL];

        //private static string[] tip     = new string[MAX_ABILITY_LEVEL];
        //private static string[] ubertip = new string[MAX_ABILITY_LEVEL];

        private static int index;
        public static int nodeIndex;
        public static int[] currentTableX = new int[8];
        public static string abilityName;
        public static string abilityStructName;
        public static string abilitySuffix;

        /*public static string RichTextBoxFormat()
        {
            return "|c00FEBA0EName:|r             " + WTS.Get(mapName) + "|n|n" +
                   "|c00FEBA0EPlayers:|r           " + WTS.Get(mapPlayers) + "|n|n" +
                   "|c00FEBA0EAutor:|r             " + WTS.Get(mapAutor) + "|n|n" +
                   "|c00FEBA0EDescription:|r|n|n" + WTS.Get(mapDescription);
        }*/

        public static void Init()
        {
            lvl[0, 1] = AbilityData.Field.Cool1;
            lvl[0, 2] = AbilityData.Field.Cool2;
            lvl[0, 3] = AbilityData.Field.Cool3;
            lvl[0, 4] = AbilityData.Field.Cool4;

            lvl[1, 1] = AbilityData.Field.Cost1;
            lvl[1, 2] = AbilityData.Field.Cost2;
            lvl[1, 3] = AbilityData.Field.Cost3;
            lvl[1, 4] = AbilityData.Field.Cost4;

            lvl[2, 1] = AbilityData.Field.Rng1;
            lvl[2, 2] = AbilityData.Field.Rng2;
            lvl[2, 3] = AbilityData.Field.Rng3;
            lvl[2, 4] = AbilityData.Field.Rng4;

            lvl[3, 1] = AbilityData.Field.Area1;
            lvl[3, 2] = AbilityData.Field.Area2;
            lvl[3, 3] = AbilityData.Field.Area3;
            lvl[3, 4] = AbilityData.Field.Area4;

            lvl[4, 1] = AbilityData.Field.Cast1;
            lvl[4, 2] = AbilityData.Field.Cast2;
            lvl[4, 3] = AbilityData.Field.Cast3;
            lvl[4, 4] = AbilityData.Field.Cast4;

            lvl[5, 1] = AbilityData.Field.HeroDur1;
            lvl[5, 2] = AbilityData.Field.HeroDur2;
            lvl[5, 3] = AbilityData.Field.HeroDur3;
            lvl[5, 4] = AbilityData.Field.HeroDur4;

            lvl[6, 1] = AbilityData.Field.Dur1;
            lvl[6, 2] = AbilityData.Field.Dur2;
            lvl[6, 3] = AbilityData.Field.Dur3;
            lvl[6, 4] = AbilityData.Field.Dur4;
        }

        public static void PrintAbilityInfoFromNode(TreeNode node)
        {
            nodeIndex = Convert.ToInt32(node.Name);

            abilityName = node.ToString().Replace("TreeNode: ", null);
            abilityStructName = AbilityNameToAbilityStructName(DataField.GetValue(AbilityData.table[AbilityData.Field.alias, nodeIndex].Replace("\"", null), DataField.Name, AbilityData.StringList));
            abilitySuffix = DataField.GetValue(AbilityData.table[AbilityData.Field.alias, nodeIndex].Replace("\"", null), DataField.EditorSuffix, AbilityData.StringList);

            //Read ability levels
            /*int levels = (int)AbilityData.ReadDecimal(AbilityData.Field.levels, nodeIndex);
            SetValue(Wc3Engine.This.levels_numericUpDown, AbilityData.Field.levels);
            Wc3Engine.This.levelParameters_comboBox.Items.Clear();
            for (int i = 1; i < levels + 1; i++)
                Wc3Engine.This.levelParameters_comboBox.Items.Add(i.ToString());
            if (0 < levels)
                Wc3Engine.This.levelParameters_comboBox.SelectedIndex = 0;

            ShowDataOfLvl(1);

            //Basics
            Wc3Engine.This.requiredLevel_numericUpDown.Value = AbilityData.ReadInt(AbilityData.Field.reqLevel, nodeIndex);
            
            Tooltip.Print(Wc3Engine.This.abilityDescription_readOnlyRichTextBox, abilityName, true);*/
        }

        private static string AbilityNameToAbilityStructName(string source)
        {
            string result = "";
            for (int i = 1; i < source.Length + 1; i++)
            {
                if (Char.IsWhiteSpace(source[i - 1]))
                {
                    result += source[i].ToString().ToUpper();
                    i++;
                }
                else
                    result += source[i - 1];
            }
            return new string(result.ToCharArray().Where(c => Char.IsLetter(c)).ToArray()).RemoveDiacritics();
        }

        public static void ShowDataOfLvl(int level)
        {
            //Level parameters
            index = 0;
            /*SetValue(Wc3Engine.This.cooldown_numericUpDown, lvl[0, level]);
            SetValue(Wc3Engine.This.manaCost_numericUpDown, lvl[1, level]);
            SetValue(Wc3Engine.This.range_numericUpDown, lvl[2, level]);
            SetValue(Wc3Engine.This.aoe_numericUpDown, lvl[3, level]);
            SetValue(Wc3Engine.This.castDelay_numericUpDown, lvl[4, level]);
            SetValue(Wc3Engine.This.durationHero_numericUpDown, lvl[5, level]);
            SetValue(Wc3Engine.This.durationNormal_numericUpDown, lvl[6, level]);*/
        }

        private static void SetValue(NumericUpDown numeric, int x)
        {
            numeric.Tag = index;
            currentTableX[index] = x;
            numeric.Value = AbilityData.ReadDecimal(x, nodeIndex);
            index++;
        }

        #region Ability Helper
        public static void NewAbility()
        {

        }
        #endregion
        /*
        #region Missile Helper
        private static AbilityStruct MissileStruct;

        public static void UpdateMissileHandle()
        {
            if (!MissileStruct.Exist())
                MissileStruct = new AbilityStruct(abilityStructName, abilitySuffix);

            if (!MissileStruct.FunctionExist("OnCast"))
                MissileStruct.InsertFunction(MissileStruct.Count, "OnCast", "nothing");

            //if (0 < Wc3Engine.This.missileCount_numericUpDown.Value)
            //{
                //Jass.InsertFunctionToAbilityStruct("OnCast", abilityStructName, "Init", "nothing", "nothing");
                //Jass.InsertCallToFunction(abilityStructName + "OnCast", "wtf??");
            //}
        }
        #endregion*/
    }
}
