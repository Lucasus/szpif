namespace Szpif.Forms.ContentForms
{
    partial class CalendarForm
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
			this.monthCalendar = new System.Windows.Forms.MonthCalendar();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// monthCalendar
			// 
			this.monthCalendar.Location = new System.Drawing.Point(2, 1);
			this.monthCalendar.Name = "monthCalendar";
			this.monthCalendar.TabIndex = 9;
			this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
			this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(2, 158);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(164, 23);
			this.button1.TabIndex = 10;
			this.button1.Text = "Anuluj";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// CalendarForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(165, 184);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.monthCalendar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CalendarForm";
			this.ShowInTaskbar = false;
			this.Text = "CalendarForm";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button button1;
    }
}