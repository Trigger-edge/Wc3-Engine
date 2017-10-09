using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class ReadOnlyRichTextBox : RichTextBox
{

    [DllImport("user32.dll")]
    private static extern int HideCaret(IntPtr hwnd);

    private bool m_ReadOnlyDeselect = false;

    public ReadOnlyRichTextBox()
    {
        MouseMove += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
        MouseDown += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
        MouseUp += new MouseEventHandler(ReadOnlyRichTextBox_Mouse);
        base.ReadOnly = true;
        base.TabStop = false;
    }

    private void MakeReadOnly()
    {
        if (base.ReadOnly)
        {
            if (m_ReadOnlyDeselect)
                DeselectAll();

            HideCaret(Handle);
        }
    }

    protected override void OnGotFocus(EventArgs e)
    {
        MakeReadOnly();
    }

    protected override void OnEnter(EventArgs e)
    {
        MakeReadOnly();
    }

    [DefaultValue(true)]
    public new bool ReadOnly
    {
        get { return base.ReadOnly; }
        set { base.ReadOnly = value; }
    }

    [DefaultValue(false)]
    public new bool TabStop
    {
        get { return false; }
        set { }
    }

    [DefaultValue(false)]
    public bool ReadOnlyDeselect
    {
        get { return m_ReadOnlyDeselect; }
        set { m_ReadOnlyDeselect = value; }
    }

    private void ReadOnlyRichTextBox_Mouse(object sender, MouseEventArgs e)
    {
        MakeReadOnly();
    }
}