namespace Szpif
{
    partial class StateControl
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
            this.columnValue = new System.Windows.Forms.ComboBox();
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
            // columnValue
            // 
            this.columnValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.columnValue.FormattingEnabled = true;
            this.columnValue.Location = new System.Drawing.Point(118, 6);
            this.columnValue.Name = "columnValue";
            this.columnValue.Size = new System.Drawing.Size(178, 21);
            this.columnValue.TabIndex = 2;
            // 
            // StateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.columnValue);
            this.Controls.Add(this.ColumnNameLabel);
            this.Name = "StateControl";
            this.Size = new System.Drawing.Size(299, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ColumnNameLabel;
        private System.Windows.Forms.ComboBox columnValue;
    }
}
