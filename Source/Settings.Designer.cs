namespace Wc3Engine
{
    partial class Settings
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
            this.gamePathTextBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gamePathTextBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveLastMap = new System.Windows.Forms.CheckBox();
            this.button_addCmd = new System.Windows.Forms.Button();
            this.button_removeCmd = new System.Windows.Forms.Button();
            this.gamePathFileOpen1 = new System.Windows.Forms.Button();
            this.gamePathFileOpen2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmd_listBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.settingsCloseButton = new System.Windows.Forms.Button();
            this.gamePathOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cmd_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gamePathTextBox1
            // 
            this.gamePathTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.gamePathTextBox1.Location = new System.Drawing.Point(157, 19);
            this.gamePathTextBox1.Name = "gamePathTextBox1";
            this.gamePathTextBox1.ReadOnly = true;
            this.gamePathTextBox1.Size = new System.Drawing.Size(254, 20);
            this.gamePathTextBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wc3 Path -1.27b (required)";
            // 
            // gamePathTextBox2
            // 
            this.gamePathTextBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.gamePathTextBox2.Location = new System.Drawing.Point(157, 44);
            this.gamePathTextBox2.Name = "gamePathTextBox2";
            this.gamePathTextBox2.ReadOnly = true;
            this.gamePathTextBox2.Size = new System.Drawing.Size(254, 20);
            this.gamePathTextBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Wc3 Path +1.27b (optional)";
            // 
            // saveLastMap
            // 
            this.saveLastMap.AutoSize = true;
            this.saveLastMap.Location = new System.Drawing.Point(27, 21);
            this.saveLastMap.Name = "saveLastMap";
            this.saveLastMap.Size = new System.Drawing.Size(123, 17);
            this.saveLastMap.TabIndex = 5;
            this.saveLastMap.Text = "Load last map on init";
            this.saveLastMap.UseVisualStyleBackColor = true;
            this.saveLastMap.CheckedChanged += new System.EventHandler(this.checkBox_LoadLastMap_Changed);
            // 
            // button_addCmd
            // 
            this.button_addCmd.Location = new System.Drawing.Point(265, 166);
            this.button_addCmd.Name = "button_addCmd";
            this.button_addCmd.Size = new System.Drawing.Size(33, 20);
            this.button_addCmd.TabIndex = 7;
            this.button_addCmd.Text = "->";
            this.button_addCmd.UseVisualStyleBackColor = true;
            this.button_addCmd.Click += new System.EventHandler(this.button_addCmd_Click);
            // 
            // button_removeCmd
            // 
            this.button_removeCmd.Location = new System.Drawing.Point(265, 192);
            this.button_removeCmd.Name = "button_removeCmd";
            this.button_removeCmd.Size = new System.Drawing.Size(33, 20);
            this.button_removeCmd.TabIndex = 8;
            this.button_removeCmd.Text = "<-";
            this.button_removeCmd.UseVisualStyleBackColor = true;
            this.button_removeCmd.Click += new System.EventHandler(this.button_removeCmd_Click);
            // 
            // gamePathFileOpen1
            // 
            this.gamePathFileOpen1.Location = new System.Drawing.Point(417, 18);
            this.gamePathFileOpen1.Name = "gamePathFileOpen1";
            this.gamePathFileOpen1.Size = new System.Drawing.Size(26, 20);
            this.gamePathFileOpen1.TabIndex = 9;
            this.gamePathFileOpen1.Text = "...";
            this.gamePathFileOpen1.UseVisualStyleBackColor = true;
            this.gamePathFileOpen1.Click += new System.EventHandler(this.gamePathFileOpen1_Click);
            // 
            // gamePathFileOpen2
            // 
            this.gamePathFileOpen2.Location = new System.Drawing.Point(417, 44);
            this.gamePathFileOpen2.Name = "gamePathFileOpen2";
            this.gamePathFileOpen2.Size = new System.Drawing.Size(26, 20);
            this.gamePathFileOpen2.TabIndex = 10;
            this.gamePathFileOpen2.Text = "...";
            this.gamePathFileOpen2.UseVisualStyleBackColor = true;
            this.gamePathFileOpen2.Click += new System.EventHandler(this.gamePathFileOpen2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gamePathFileOpen2);
            this.groupBox1.Controls.Add(this.gamePathTextBox1);
            this.groupBox1.Controls.Add(this.gamePathFileOpen1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.gamePathTextBox2);
            this.groupBox1.Location = new System.Drawing.Point(8, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 79);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmd_listBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(8, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(465, 116);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cmd lines";
            // 
            // cmd_listBox
            // 
            this.cmd_listBox.FormattingEnabled = true;
            this.cmd_listBox.Location = new System.Drawing.Point(296, 19);
            this.cmd_listBox.Name = "cmd_listBox";
            this.cmd_listBox.Size = new System.Drawing.Size(158, 82);
            this.cmd_listBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Command lines that run when test a map";
            // 
            // settingsCloseButton
            // 
            this.settingsCloseButton.Location = new System.Drawing.Point(412, 251);
            this.settingsCloseButton.Name = "settingsCloseButton";
            this.settingsCloseButton.Size = new System.Drawing.Size(61, 19);
            this.settingsCloseButton.TabIndex = 13;
            this.settingsCloseButton.Text = "Close";
            this.settingsCloseButton.UseVisualStyleBackColor = true;
            this.settingsCloseButton.Click += new System.EventHandler(this.settingsCloseButton_Click);
            // 
            // gamePathOpenFileDialog
            // 
            this.gamePathOpenFileDialog.Filter = "|*War3.exe; *Warcraft III.exe";
            // 
            // cmd_textBox
            // 
            this.cmd_textBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmd_textBox.Location = new System.Drawing.Point(27, 167);
            this.cmd_textBox.Name = "cmd_textBox";
            this.cmd_textBox.Size = new System.Drawing.Size(232, 20);
            this.cmd_textBox.TabIndex = 6;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 278);
            this.Controls.Add(this.settingsCloseButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_removeCmd);
            this.Controls.Add(this.button_addCmd);
            this.Controls.Add(this.cmd_textBox);
            this.Controls.Add(this.saveLastMap);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox gamePathTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gamePathTextBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox saveLastMap;
        private System.Windows.Forms.Button button_addCmd;
        private System.Windows.Forms.Button button_removeCmd;
        private System.Windows.Forms.Button gamePathFileOpen1;
        private System.Windows.Forms.Button gamePathFileOpen2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button settingsCloseButton;
        private System.Windows.Forms.OpenFileDialog gamePathOpenFileDialog;
        private System.Windows.Forms.TextBox cmd_textBox;
        private System.Windows.Forms.ListBox cmd_listBox;
    }
}