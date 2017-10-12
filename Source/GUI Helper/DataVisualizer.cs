using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Wc3Engine
{
    public partial class DataVisualizer : UserControl
    {
        private const int TableLayoutSizeColumn1 = 150;
        private const int TableLayoutSizeColumn2 = 200;

        public static TableLayoutPanel mainPanel;
  
        public DataVisualizer(string visualizerName, StreamReader script)
        {
            InitializeComponent();

            Title.Text = visualizerName;

            mainPanel.RowCount = mainPanel.RowCount + 1;
            mainPanel.Controls.Add(this, 0, mainPanel.RowCount);

            //ReadVariablesFromScript(script);
            /*
            int y = 3;
            int rows = tableLayoutPanel.RowCount - 1;

            for (int i = 0; i < numericCount; i++)
            {
                Label label = new Label();
                TextBox box = new TextBox();

                rows++;
                tableLayoutPanel.RowCount = rows;
                tableLayoutPanel.Controls.Add(label, 0, rows) ;
                tableLayoutPanel.Controls.Add(box, 1, rows);

                label.Text = "var name test" + i.ToString();
                label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                label.AutoEllipsis = true;
                label.Location = new Point(3, y + 4);
                label.Size = new Size(TableLayoutSizeColumn1, 20);

                box.Anchor = AnchorStyles.Bottom;
                box.Location = new Point(3, y);
                box.Size = new Size(TableLayoutSizeColumn2, 20);

                y = y + 22;
            }*/

            //script.Dispose();
        }

        /*private void ReadVariablesFromScript(StreamReader script)
        {
            for (int i = 0; ; i++)
            {
                string line = script.ReadLine();
            }
        }*/

        private void OnHeaderClick(object sender, EventArgs e)
        {
            if (groupBox.Visible)
                arrowBox.Image = Properties.Resources.arrow_up;
            else
                arrowBox.Image = Properties.Resources.arrow_down;

            groupBox.Visible = !groupBox.Visible;
        }
    }
}