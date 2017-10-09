using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Wc3Engine
{
    public class Tooltip
    {

        private static List<string> list     = new List<string>();
        private static List<string> argbList = new List<string>();

        private static Font normalFont = new Font("Tahoma", 9, FontStyle.Regular);
        private static Font boldFont   = new Font("Tahoma", 9, FontStyle.Bold);

        private static Color defaultColor = Color.WhiteSmoke;


        public static void Print(RichTextBox box, string text, bool bold)
        {
            while (text.Contains("|n"))
                text = text.Replace("|n", Environment.NewLine);

            while (text.Contains("|c") && text.Contains("|r"))
            {
                int startIndex = text.IndexOf("|c");
                int endIndex = text.IndexOf("|r");

                if (0 > startIndex && 0 > endIndex)
                    break;

                string coloredString = text.Substring(startIndex, endIndex - startIndex + 2);

                string output = coloredString.Replace(coloredString.Substring(0, 10), null);
                output = output.Replace("|r", null);

                text = text.Replace(coloredString, output);

                list.Add(output);
                argbList.Add(coloredString.Substring(2, 8));
            }

            box.Text = text;

            box.SelectAll();

            if (bold)
                box.SelectionFont = boldFont;
            else
                box.SelectionFont = normalFont;

            box.SelectionColor = defaultColor;
            box.SelectionAlignment = HorizontalAlignment.Left;

            foreach (string colored in list)
            {
                box.Select(box.Find(colored), colored.Length);

                if (bold)
                    box.SelectionFont = boldFont;
                else
                    box.SelectionFont = normalFont;

                box.SelectionColor = Color.FromArgb(Convert.ToInt32(argbList[list.IndexOf(colored)], 16));
            }

            list.Clear();
            argbList.Clear();
            //list = null;
            //argbList = null;

            box.DeselectAll();
        }
    }
}
