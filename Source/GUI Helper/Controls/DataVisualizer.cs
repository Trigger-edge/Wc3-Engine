using System.Windows.Forms;

namespace Wc3Engine
{
    public partial class DataVisualizer : UserControl
    {
        private const int MaxVarLenght      = 70;
        private const int MaxBoxValueSize   = 200;

        public static TableLayoutPanel panel;
  
        public DataVisualizer(string visualizerName, int numericCount)
        {
            InitializeComponent();

            Title.Text = visualizerName;

            panel.RowCount = panel.RowCount + 1;
            panel.Controls.Add(this, 0, panel.RowCount);

            int y = 3;

            for (int i = 0; i < numericCount; i++)
            {
                Label label = new Label();
                TextBox box = new TextBox();

                panel1.Controls.Add(label);
                panel2.Controls.Add(box);

                SetVarName(label, "var name " + i.ToString());
                //label.Text = "var name " + i.ToString();
                //while (label.Text.Length < MaxVarLenght)
                //label.Text = label.Text + "_";
                label.AutoEllipsis = true;
                label.Location = new System.Drawing.Point(3, y + 4);
                label.Size = new System.Drawing.Size(MaxBoxValueSize, 20);

                box.Location = new System.Drawing.Point(3, y);
                box.Size = new System.Drawing.Size(MaxBoxValueSize, 20);

                y = y + 22;
            }
        }

        private static void SetVarName(Label label, string text)
        {
            //if (text.Length < MaxVarLenght)
            //{
                while (text.Length < MaxVarLenght)
                    text = text + "c";
                //for (int i = text.Length; i < MaxVarLenght; i++)
                    //text = text + "c";
            //}

            //Wc3Engine.DebugMsg(text);
            label.Text = text;
        }

        private void OnTitleClick(object sender, System.EventArgs e)
        {
            groupBox.Visible = !groupBox.Visible;
        }
    }
}