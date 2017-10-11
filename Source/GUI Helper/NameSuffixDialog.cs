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
            Ability ability = Wc3Engine.This.SelectedAbility;

            ability.Name = Name_textBox.Text;
            ability.Suffix = Suffix_textBox.Text;

            ability.Model.Name = "(" + ability.CodeId + ") " + Name_textBox.Text;
            ability.Model.Suffix = Suffix_textBox.Text;

            Wc3Engine.This.LTVCustomAbilities.RefreshObject(Wc3Engine.This.SelectedAbility.Model);
            Close();
        }
    }
}
