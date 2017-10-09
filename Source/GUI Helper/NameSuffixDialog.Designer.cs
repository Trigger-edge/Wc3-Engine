namespace Wc3Engine
{
    partial class NameSuffixDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.Suffix_textBox = new System.Windows.Forms.TextBox();
            this.Name_label = new System.Windows.Forms.Label();
            this.Suffix_label = new System.Windows.Forms.Label();
            this.Ok_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Name_textBox
            // 
            this.Name_textBox.Location = new System.Drawing.Point(54, 12);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(174, 20);
            this.Name_textBox.TabIndex = 0;
            // 
            // Suffix_textBox
            // 
            this.Suffix_textBox.Location = new System.Drawing.Point(54, 38);
            this.Suffix_textBox.Name = "Suffix_textBox";
            this.Suffix_textBox.Size = new System.Drawing.Size(174, 20);
            this.Suffix_textBox.TabIndex = 1;
            // 
            // Name_label
            // 
            this.Name_label.AutoSize = true;
            this.Name_label.Location = new System.Drawing.Point(12, 15);
            this.Name_label.Name = "Name_label";
            this.Name_label.Size = new System.Drawing.Size(38, 13);
            this.Name_label.TabIndex = 2;
            this.Name_label.Text = "Name:";
            // 
            // Suffix_label
            // 
            this.Suffix_label.AutoSize = true;
            this.Suffix_label.Location = new System.Drawing.Point(12, 41);
            this.Suffix_label.Name = "Suffix_label";
            this.Suffix_label.Size = new System.Drawing.Size(36, 13);
            this.Suffix_label.TabIndex = 3;
            this.Suffix_label.Text = "Suffix:";
            // 
            // Ok_button
            // 
            this.Ok_button.Location = new System.Drawing.Point(110, 64);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(56, 20);
            this.Ok_button.TabIndex = 4;
            this.Ok_button.Text = "Ok";
            this.Ok_button.UseVisualStyleBackColor = true;
            this.Ok_button.Click += new System.EventHandler(this.Ok_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(172, 64);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(56, 20);
            this.Cancel_button.TabIndex = 5;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // NameSuffixDialog
            // 
            this.AcceptButton = this.Ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(244, 93);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Ok_button);
            this.Controls.Add(this.Suffix_label);
            this.Controls.Add(this.Name_label);
            this.Controls.Add(this.Suffix_textBox);
            this.Controls.Add(this.Name_textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NameSuffixDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Name_textBox;
        private System.Windows.Forms.TextBox Suffix_textBox;
        private System.Windows.Forms.Label Name_label;
        private System.Windows.Forms.Label Suffix_label;
        private System.Windows.Forms.Button Ok_button;
        private System.Windows.Forms.Button Cancel_button;
    }
}