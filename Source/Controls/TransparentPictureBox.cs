using System.Drawing;
using System.Windows.Forms;

public class TransparentPictureBox : Control
{
    public TransparentPictureBox()
    {
        /*MouseMove += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
        MouseDown += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
        MouseUp += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);*/

        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        //SizeMode = PictureBoxSizeMode.StretchImage;
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
        //if (Image != null)
            //Launcher.DebugMsg("test");
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        //if (BackgroundImage != null)
            //BackgroundImage.Dispose();

        //BackgroundImage = new Bitmap(Width, Height);

        /*if (null != BackgroundImage)
        {
            Graphics g = Graphics.FromImage(BackgroundImage);

            Rectangle rct = new Rectangle(0, 0, Width, Height);

            g.DrawImage(BackgroundImage, rct);

            g.Dispose();
            //BackgroundImage.Dispose();
        }*/
    }
    
    /*[DefaultValue(PictureBoxSizeMode.StretchImage)]
    public new PictureBoxSizeMode SizeMode
    {
        get { return base.SizeMode; }
        set { base.SizeMode = value; }
    }*/
}
