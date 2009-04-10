using System;
namespace Interface
{
    partial class ChangeEmployeeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RoleCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.deleteEmployeeButton = new System.Windows.Forms.Button();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.NazwaTextBox = new System.Windows.Forms.TextBox();
            this.EMailTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "E-mail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nazwa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Role";
            // 
            // RoleCheckedListBox
            // 
            this.RoleCheckedListBox.FormattingEnabled = true;
            this.RoleCheckedListBox.Items.AddRange(new object[] {
            "Właściciel",
            "Project Manager",
            "Przełożony",
            "Zwykły pracownik",
            "Opiekun handlowy"});
            this.RoleCheckedListBox.Location = new System.Drawing.Point(89, 110);
            this.RoleCheckedListBox.Name = "RoleCheckedListBox";
            this.RoleCheckedListBox.Size = new System.Drawing.Size(120, 79);
            this.RoleCheckedListBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Nowe hasło";
            // 
            // deleteEmployeeButton
            // 
            this.deleteEmployeeButton.Location = new System.Drawing.Point(110, 195);
            this.deleteEmployeeButton.Name = "deleteEmployeeButton";
            this.deleteEmployeeButton.Size = new System.Drawing.Size(99, 23);
            this.deleteEmployeeButton.TabIndex = 6;
            this.deleteEmployeeButton.Text = "Usuń pracownika";
            this.deleteEmployeeButton.UseVisualStyleBackColor = true;
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(89, 15);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(120, 20);
            this.LoginTextBox.TabIndex = 7;
            // 
            // NazwaTextBox
            // 
            this.NazwaTextBox.Location = new System.Drawing.Point(89, 39);
            this.NazwaTextBox.Name = "NazwaTextBox";
            this.NazwaTextBox.Size = new System.Drawing.Size(120, 20);
            this.NazwaTextBox.TabIndex = 8;
            // 
            // EMailTextBox
            // 
            this.EMailTextBox.Location = new System.Drawing.Point(89, 62);
            this.EMailTextBox.Name = "EMailTextBox";
            this.EMailTextBox.Size = new System.Drawing.Size(120, 20);
            this.EMailTextBox.TabIndex = 9;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(89, 84);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(120, 20);
            this.PasswordTextBox.TabIndex = 10;
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(17, 196);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(87, 23);
            this.SaveChangesButton.TabIndex = 11;
            this.SaveChangesButton.Text = "Zapisz zmiany";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // ChangeEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 223);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.EMailTextBox);
            this.Controls.Add(this.NazwaTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.deleteEmployeeButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RoleCheckedListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChangeEmployeeForm";
            this.Text = "Edycja danych pracownika";
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox RoleCheckedListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button deleteEmployeeButton;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.TextBox NazwaTextBox;
        private System.Windows.Forms.TextBox EMailTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button SaveChangesButton;
    }
}