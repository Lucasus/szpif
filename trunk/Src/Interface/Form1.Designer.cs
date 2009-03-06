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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UserNameTextBox
			// 
			this.UserNameTextBox.Location = new System.Drawing.Point(29, 69);
			this.UserNameTextBox.Name = "UserNameTextBox";
			this.UserNameTextBox.Size = new System.Drawing.Size(230, 20);
			this.UserNameTextBox.TabIndex = 0;
			// 
			// PassWordTextBox
			// 
			this.PassWordTextBox.AccessibleName = "";
			this.PassWordTextBox.Location = new System.Drawing.Point(29, 106);
			this.PassWordTextBox.Name = "PassWordTextBox";
			this.PassWordTextBox.Size = new System.Drawing.Size(230, 20);
			this.PassWordTextBox.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.AccessibleName = "AcceptButton";
			this.button1.Location = new System.Drawing.Point(29, 146);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(81, 22);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.PassWordTextBox);
			this.Controls.Add(this.UserNameTextBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.TextBox PassWordTextBox;
		private System.Windows.Forms.Button button1;
    }
}

