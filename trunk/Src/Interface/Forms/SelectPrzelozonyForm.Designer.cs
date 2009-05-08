namespace Szpif
{
    partial class SelectPrzelozonyForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.selectPrzelozonyGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.selectPrzelozonyGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(120, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Anuluj";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // selectPrzelozonyGridView
            // 
            this.selectPrzelozonyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectPrzelozonyGridView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.selectPrzelozonyGridView.Location = new System.Drawing.Point(12, 12);
            this.selectPrzelozonyGridView.Name = "selectPrzelozonyGridView";
            this.selectPrzelozonyGridView.Size = new System.Drawing.Size(196, 218);
            this.selectPrzelozonyGridView.TabIndex = 2;
            this.selectPrzelozonyGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectPrzelozonyGridView_CellClick);
            // 
            // SelectPrzelozonyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 268);
            this.Controls.Add(this.selectPrzelozonyGridView);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "SelectPrzelozonyForm";
            this.Text = "SelectPrzelozonyForm";
            ((System.ComponentModel.ISupportInitialize)(this.selectPrzelozonyGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView selectPrzelozonyGridView;
    }
}