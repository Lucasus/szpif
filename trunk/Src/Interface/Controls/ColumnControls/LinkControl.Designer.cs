namespace Szpif
{
    partial class LinkControl
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
            this.ColumnValue = new System.Windows.Forms.TextBox();
            this.ColumnNameLabel = new System.Windows.Forms.Label();
            this.SelectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ColumnValue
            // 
            this.ColumnValue.Location = new System.Drawing.Point(117, 3);
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.Size = new System.Drawing.Size(144, 20);
            this.ColumnValue.TabIndex = 3;
            // 
            // ColumnNameLabel
            // 
            this.ColumnNameLabel.AutoSize = true;
            this.ColumnNameLabel.Location = new System.Drawing.Point(2, 6);
            this.ColumnNameLabel.Name = "ColumnNameLabel";
            this.ColumnNameLabel.Size = new System.Drawing.Size(35, 13);
            this.ColumnNameLabel.TabIndex = 2;
            this.ColumnNameLabel.Text = "label1";
            // 
            // SelectButton
            // 
            this.SelectButton.Location = new System.Drawing.Point(267, 3);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(31, 20);
            this.SelectButton.TabIndex = 4;
            this.SelectButton.Text = "...";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // LinkControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.ColumnValue);
            this.Controls.Add(this.ColumnNameLabel);
            this.Name = "LinkControl";
            this.Size = new System.Drawing.Size(301, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ColumnValue;
        private System.Windows.Forms.Label ColumnNameLabel;
        private System.Windows.Forms.Button SelectButton;
    }
}
