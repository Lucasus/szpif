namespace Szpif
{
    partial class DefaultSzpifControl
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
            this.ColumnNameLabel = new System.Windows.Forms.Label();
            this.ColumnValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ColumnNameLabel
            // 
            this.ColumnNameLabel.AutoSize = true;
            this.ColumnNameLabel.Location = new System.Drawing.Point(3, 6);
            this.ColumnNameLabel.Name = "ColumnNameLabel";
            this.ColumnNameLabel.Size = new System.Drawing.Size(35, 13);
            this.ColumnNameLabel.TabIndex = 0;
            this.ColumnNameLabel.Text = "label1";
            // 
            // ColumnValue
            // 
            this.ColumnValue.Location = new System.Drawing.Point(118, 3);
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.Size = new System.Drawing.Size(178, 20);
            this.ColumnValue.TabIndex = 1;
            // 
            // DefaultSzpifControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ColumnValue);
            this.Controls.Add(this.ColumnNameLabel);
            this.Name = "DefaultSzpifControl";
            this.Size = new System.Drawing.Size(299, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ColumnNameLabel;
        private System.Windows.Forms.TextBox ColumnValue;
    }
}
