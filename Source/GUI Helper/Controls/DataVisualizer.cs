using System.Windows.Forms;

namespace Wc3Engine
{
    public partial class DataVisualizer : UserControl
    {
        //public NumericUpDown[] numeric;

        public DataVisualizer(int numericCount)
        {
            InitializeComponent();

            int y = 3;

            for (int i = 0; i < numericCount; i++)
            {
                TextBox box = new TextBox();
                splitContainer.Panel2.Controls.Add(box);

                box.Location = new System.Drawing.Point(3, y);
                box.Size = new System.Drawing.Size(118, 20);

                y = y + 20;
            }
        }
    }
}