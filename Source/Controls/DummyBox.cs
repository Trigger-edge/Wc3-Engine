using System.Windows.Forms;
using System.Drawing;


public class DummyBox : Control
{

    public DummyBox()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20;
            return cp;
        }
    }

    
    protected override void OnPaint(PaintEventArgs e)
    {
        //Do not paint background
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        //Do not paint background
    }
}