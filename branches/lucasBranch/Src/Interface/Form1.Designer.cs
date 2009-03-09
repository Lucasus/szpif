namespace Interface
{
    partial class Form1
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
			this.UserNameTextBox = new System.Windows.Forms.TextBox();
			this.PassWordTextBox = new System.Windows.Forms.TextBox();
			this.RankTextBox = new System.Windows.Forms.TextBox();
			this.logInButton = new System.Windows.Forms.Button();
			this.UserNameLabel = new System.Windows.Forms.Label();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.RankLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// UserNameTextBox
			// 
			this.UserNameTextBox.Location = new System.Drawing.Point(81, 80);
			this.UserNameTextBox.Name = "UserNameTextBox";
			this.UserNameTextBox.Size = new System.Drawing.Size(178, 20);
			this.UserNameTextBox.TabIndex = 0;
			// 
			// PassWordTextBox
			// 
			this.PassWordTextBox.AccessibleName = "";
			this.PassWordTextBox.Location = new System.Drawing.Point(81, 106);
			this.PassWordTextBox.Name = "PassWordTextBox";
			this.PassWordTextBox.Size = new System.Drawing.Size(178, 20);
			this.PassWordTextBox.TabIndex = 1;
			// 
			// RankTextBox
			// 
			this.RankTextBox.Location = new System.Drawing.Point(81, 132);
			this.RankTextBox.Name = "RankTextBox";
			this.RankTextBox.Size = new System.Drawing.Size(178, 20);
			this.RankTextBox.TabIndex = 2;
			// 
			// logInButton
			// 
			this.logInButton.AccessibleName = "AcceptButton";
			this.logInButton.Location = new System.Drawing.Point(81, 182);
			this.logInButton.Name = "logInButton";
			this.logInButton.Size = new System.Drawing.Size(81, 22);
			this.logInButton.TabIndex = 3;
			this.logInButton.Text = "Zaloguj";
			this.logInButton.UseVisualStyleBackColor = true;
			this.logInButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// UserNameLabel
			// 
			this.UserNameLabel.AutoSize = true;
			this.UserNameLabel.Location = new System.Drawing.Point(12, 83);
			this.UserNameLabel.Name = "UserNameLabel";
			this.UserNameLabel.Size = new System.Drawing.Size(63, 13);
			this.UserNameLabel.TabIndex = 4;
			this.UserNameLabel.Text = "User Name:";
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.AutoSize = true;
			this.PasswordLabel.Location = new System.Drawing.Point(19, 109);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
			this.PasswordLabel.TabIndex = 5;
			this.PasswordLabel.Text = "Password:";
			// 
			// RankLabel
			// 
			this.RankLabel.AutoSize = true;
			this.RankLabel.Location = new System.Drawing.Point(39, 135);
			this.RankLabel.Name = "RankLabel";
			this.RankLabel.Size = new System.Drawing.Size(36, 13);
			this.RankLabel.TabIndex = 6;
			this.RankLabel.Text = "Rank:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.RankLabel);
			this.Controls.Add(this.PasswordLabel);
			this.Controls.Add(this.UserNameLabel);
			this.Controls.Add(this.RankTextBox);
			this.Controls.Add(this.PassWordTextBox);
			this.Controls.Add(this.UserNameTextBox);
			this.Controls.Add(this.logInButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.TextBox PassWordTextBox;
		private System.Windows.Forms.Button logInButton;
		private System.Windows.Forms.TextBox RankTextBox;
		private System.Windows.Forms.Label UserNameLabel;
		private System.Windows.Forms.Label PasswordLabel;
		private System.Windows.Forms.Label RankLabel;
    }
}

