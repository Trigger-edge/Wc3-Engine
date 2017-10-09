using System.Windows.Forms;

namespace Wc3Engine
{
    public partial class NameSuffixDialog : Form
    {
        public NameSuffixDialog()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, System.EventArgs e)
        {
            Name_textBox.Text = Wc3Engine.This.SelectedAbility.Name;
            Suffix_textBox.Text = Wc3Engine.This.SelectedAbility.Suffix;
        }

        private void Ok_button_Click(object sender, System.EventArgs e)
        {
            Wc3Engine.This.SelectedAbility.Name = Name_textBox.Text;
            Wc3Engine.This.SelectedAbility.Suffix = Suffix_textBox.Text;

            Wc3Engine.This.LTVCustomAbilities.UpdateObjects(Wc3Engine.CustomAbilitiesTab.MasterList);
            Close();
        }
    }
}
