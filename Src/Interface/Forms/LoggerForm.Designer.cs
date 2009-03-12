namespace Interface
{
    partial class LoggerForm
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
            this.logInButton = new System.Windows.Forms.Button();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(81, 12);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(178, 20);
            this.UserNameTextBox.TabIndex = 0;
            // 
            // PassWordTextBox
            // 
            this.PassWordTextBox.AccessibleName = "";
            this.PassWordTextBox.Location = new System.Drawing.Point(81, 38);
            this.PassWordTextBox.Name = "PassWordTextBox";
            this.PassWordTextBox.Size = new System.Drawing.Size(178, 20);
            this.PassWordTextBox.TabIndex = 1;
            // 
            // logInButton
            // 
            this.logInButton.AccessibleName = "AcceptButton";
            this.logInButton.Location = new System.Drawing.Point(81, 76);
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
            this.UserNameLabel.Location = new System.Drawing.Point(12, 15);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(63, 13);
            this.UserNameLabel.TabIndex = 4;
            this.UserNameLabel.Text = "User Name:";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(19, 41);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.PasswordLabel.TabIndex = 5;
            this.PasswordLabel.Text = "Password:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Anuluj";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 117);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.PassWordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.logInButton);
            this.Name = "LoggerForm";
            this.Text = "Logowanie do systemu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.TextBox PassWordTextBox;
        private System.Windows.Forms.Button logInButton;
		private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Button button1;
    }
}

