namespace Szpif.Controls.ContentControls
{
    partial class ViewControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewControl));
            this.gridView = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add = new System.Windows.Forms.ToolStripButton();
            this.Save = new System.Windows.Forms.ToolStripButton();
            this.Reload = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToResizeColumns = false;
            this.gridView.AllowUserToResizeRows = false;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.gridView.Location = new System.Drawing.Point(3, 3);
            this.gridView.Name = "gridView";
            this.gridView.RowHeadersVisible = false;
            this.gridView.Size = new System.Drawing.Size(709, 159);
            this.gridView.TabIndex = 0;
            this.gridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridView_CellFormatting);
            this.gridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellContentClick);
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edycja";
            this.Edit.Name = "Edit";
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.Text = "Edytuj...";
            this.Edit.Width = 60;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add,
            this.Save,
            this.Reload});
            this.toolStrip1.Location = new System.Drawing.Point(0, 168);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(715, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Add
            // 
            this.Add.Image = ((System.Drawing.Image)(resources.GetObject("Add.Image")));
            this.Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(55, 22);
            this.Add.Text = "Dodaj";
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Save
            // 
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(58, 22);
            this.Save.Text = "Zapisz";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Reload
            // 
            this.Reload.Image = ((System.Drawing.Image)(resources.GetObject("Reload.Image")));
            this.Reload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Reload.Name = "Reload";
            this.Reload.Size = new System.Drawing.Size(67, 22);
            this.Reload.Text = "Odśwież";
            this.Reload.Click += new System.EventHandler(this.Reload_Click);
            // 
            // ViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gridView);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ViewControl";
            this.Size = new System.Drawing.Size(715, 193);
            this.VisibleChanged += new System.EventHandler(this.ViewControl_VisibleChanged);
            this.SizeChanged += new System.EventHandler(this.ViewControl_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridView;
		private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add;
        private System.Windows.Forms.ToolStripButton Save;
        private System.Windows.Forms.ToolStripButton Reload;
    }
}
