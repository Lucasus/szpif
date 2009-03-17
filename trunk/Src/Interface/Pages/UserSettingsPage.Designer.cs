namespace Interface
{
    partial class UserSettingsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.passwordChangeLabel = new System.Windows.Forms.Label();
			this.passwordChangeButton = new System.Windows.Forms.Button();
			this.passwordChangeBox = new System.Windows.Forms.TextBox();
			this.passwordStrenght = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// passwordChangeLabel
			// 
			this.passwordChangeLabel.AutoSize = true;
			this.passwordChangeLabel.Location = new System.Drawing.Point(0, 0);
			this.passwordChangeLabel.Name = "passwordChangeLabel";
			this.passwordChangeLabel.Size = new System.Drawing.Size(71, 13);
			this.passwordChangeLabel.TabIndex = 0;
			this.passwordChangeLabel.Text = "Zmień Hasło:";
			// 
			// passwordChangeButton
			// 
			this.passwordChangeButton.Location = new System.Drawing.Point(0, 0);
			this.passwordChangeButton.Name = "passwordChangeButton";
			this.passwordChangeButton.Size = new System.Drawing.Size(75, 20);
			this.passwordChangeButton.TabIndex = 0;
			this.passwordChangeButton.Text = "OK";
			this.passwordChangeButton.UseVisualStyleBackColor = true;
			this.passwordChangeButton.Click += new System.EventHandler(this.passwordChangeButton_Click);
			// 
			// passwordChangeBox
			// 
			this.passwordChangeBox.Location = new System.Drawing.Point(0, 0);
			this.passwordChangeBox.Name = "passwordChangeBox";
			this.passwordChangeBox.Size = new System.Drawing.Size(100, 20);
			this.passwordChangeBox.TabIndex = 0;
			this.passwordChangeBox.UseSystemPasswordChar = true;
			// 
			// passwordStrenght
			// 
			this.passwordStrenght.AutoSize = true;
			this.passwordStrenght.Location = new System.Drawing.Point(0, 0);
			this.passwordStrenght.Name = "passwordStrenght";
			this.passwordStrenght.Size = new System.Drawing.Size(100, 23);
			this.passwordStrenght.TabIndex = 0;
			this.passwordStrenght.Text = "label1";
			// 
			// UserSettingsPage
			// 
			this.Controls.Add(this.passwordChangeLabel);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label passwordChangeLabel;
		private System.Windows.Forms.Button passwordChangeButton;
		private System.Windows.Forms.TextBox passwordChangeBox;
		private System.Windows.Forms.Label passwordStrenght;
    }
}
