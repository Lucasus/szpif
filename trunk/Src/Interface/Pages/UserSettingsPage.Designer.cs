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
			this.passwordGroup = new System.Windows.Forms.GroupBox();
			this.passwordStrenghtText = new System.Windows.Forms.Label();
			this.passwordOldPassword = new System.Windows.Forms.TextBox();
			this.eMailGroup = new System.Windows.Forms.GroupBox();
			this.eMailChangeLabel = new System.Windows.Forms.Label();
			this.eMailChangeBox = new System.Windows.Forms.TextBox();
			this.eMailChangeButton = new System.Windows.Forms.Button();
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
			this.passwordChangeButton.TabIndex = 3;
			this.passwordChangeButton.Text = "OK";
			this.passwordChangeButton.UseVisualStyleBackColor = true;
			this.passwordChangeButton.Click += new System.EventHandler(this.passwordChangeButton_Click);
			// 
			// passwordChangeBox
			// 
			this.passwordChangeBox.Location = new System.Drawing.Point(0, 0);
			this.passwordChangeBox.MaxLength = 20;
			this.passwordChangeBox.Name = "passwordChangeBox";
			this.passwordChangeBox.Size = new System.Drawing.Size(150, 20);
			this.passwordChangeBox.TabIndex = 2;
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
			// passwordGroup
			// 
			this.passwordGroup.Location = new System.Drawing.Point(0, 0);
			this.passwordGroup.Name = "passwordGroup";
			this.passwordGroup.Size = new System.Drawing.Size(400, 100);
			this.passwordGroup.TabIndex = 0;
			this.passwordGroup.TabStop = false;
			this.passwordGroup.Text = "Zarządzanie Hasłem";
			// 
			// passwordStrenghtText
			// 
			this.passwordStrenghtText.AutoSize = true;
			this.passwordStrenghtText.Location = new System.Drawing.Point(0, 0);
			this.passwordStrenghtText.Name = "passwordStrenghtText";
			this.passwordStrenghtText.Size = new System.Drawing.Size(100, 23);
			this.passwordStrenghtText.TabIndex = 0;
			this.passwordStrenghtText.Text = "Siła twojego hasła: ";
			// 
			// passwordOldPassword
			// 
			this.passwordOldPassword.Location = new System.Drawing.Point(0, 0);
			this.passwordOldPassword.MaxLength = 20;
			this.passwordOldPassword.Name = "passwordOldPassword";
			this.passwordOldPassword.Size = new System.Drawing.Size(150, 20);
			this.passwordOldPassword.TabIndex = 1;
			this.passwordOldPassword.UseSystemPasswordChar = true;
			// 
			// eMailGroup
			// 
			this.eMailGroup.Location = new System.Drawing.Point(0, 0);
			this.eMailGroup.Name = "eMailGroup";
			this.eMailGroup.Size = new System.Drawing.Size(200, 100);
			this.eMailGroup.TabIndex = 0;
			this.eMailGroup.TabStop = false;
			this.eMailGroup.Text = "Zarządzanie E-mail\'em";
			// 
			// eMailChangeLabel
			// 
			this.eMailChangeLabel.AutoSize = true;
			this.eMailChangeLabel.Location = new System.Drawing.Point(0, 0);
			this.eMailChangeLabel.Name = "eMailChangeLabel";
			this.eMailChangeLabel.Size = new System.Drawing.Size(100, 23);
			this.eMailChangeLabel.TabIndex = 0;
			this.eMailChangeLabel.Text = "Zmień swojego E-Mail\'a: ";
			// 
			// eMailChangeBox
			// 
			this.eMailChangeBox.Location = new System.Drawing.Point(0, 0);
			this.eMailChangeBox.Name = "eMailChangeBox";
			this.eMailChangeBox.Size = new System.Drawing.Size(100, 20);
			this.eMailChangeBox.TabIndex = 0;
			// 
			// eMailChangeButton
			// 
			this.eMailChangeButton.Location = new System.Drawing.Point(0, 0);
			this.eMailChangeButton.Name = "eMailChangeButton";
			this.eMailChangeButton.Size = new System.Drawing.Size(75, 23);
			this.eMailChangeButton.TabIndex = 0;
			this.eMailChangeButton.Text = "OK";
			this.eMailChangeButton.UseVisualStyleBackColor = true;
			this.eMailChangeButton.Click += new System.EventHandler(this.eMailChangeButton_Click);
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
		private System.Windows.Forms.GroupBox passwordGroup;
		private System.Windows.Forms.Label passwordStrenghtText;
		private System.Windows.Forms.TextBox passwordOldPassword;
		private System.Windows.Forms.GroupBox eMailGroup;
		private System.Windows.Forms.Label eMailChangeLabel;
		private System.Windows.Forms.TextBox eMailChangeBox;
		private System.Windows.Forms.Button eMailChangeButton;
    }
}
