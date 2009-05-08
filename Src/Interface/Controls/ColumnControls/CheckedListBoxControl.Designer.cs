namespace Szpif
{
    partial class CheckedListBoxControl
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
            this.columnValue = new System.Windows.Forms.CheckedListBox();
            this.ColumnNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // columnValue
            // 
            this.columnValue.FormattingEnabled = true;
            this.columnValue.Location = new System.Drawing.Point(118, 3);
            this.columnValue.Name = "columnValue";
            this.columnValue.Size = new System.Drawing.Size(178, 19);
            this.columnValue.TabIndex = 0;
            // 
            // ColumnNameLabel
            // 
            this.ColumnNameLabel.AutoSize = true;
            this.ColumnNameLabel.Location = new System.Drawing.Point(3, 3);
            this.ColumnNameLabel.Name = "ColumnNameLabel";
            this.ColumnNameLabel.Size = new System.Drawing.Size(35, 13);
            this.ColumnNameLabel.TabIndex = 2;
            this.ColumnNameLabel.Text = "label1";
            // 
            // CheckedListBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColumnNameLabel);
            this.Controls.Add(this.columnValue);
            this.Name = "CheckedListBoxControl";
            this.Size = new System.Drawing.Size(315, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox columnValue;
        private System.Windows.Forms.Label ColumnNameLabel;
    }
}
