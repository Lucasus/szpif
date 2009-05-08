using System.Windows.Forms;
namespace Szpif
{
    partial class EmployeeAdministrationPage
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
            this.AddEmployeeButton = new System.Windows.Forms.Button();
            this.EmployeesForAdministrationGridView = new System.Windows.Forms.DataGridView();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.Edytuj = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RefreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesForAdministrationGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AddEmployeeButton
            // 
            this.AddEmployeeButton.Location = new System.Drawing.Point(10, 400);
            this.AddEmployeeButton.Name = "AddEmployeeButton";
            this.AddEmployeeButton.Size = new System.Drawing.Size(160, 23);
            this.AddEmployeeButton.TabIndex = 0;
            this.AddEmployeeButton.Text = "Dodaj pracownika";
            this.AddEmployeeButton.UseVisualStyleBackColor = true;
            this.AddEmployeeButton.Click += new System.EventHandler(this.AddEmployeeButton_Click);
            // 
            // EmployeesForAdministrationGridView
            // 
            this.EmployeesForAdministrationGridView.AllowUserToAddRows = false;
            this.EmployeesForAdministrationGridView.AllowUserToDeleteRows = false;
            this.EmployeesForAdministrationGridView.AllowUserToResizeColumns = false;
            this.EmployeesForAdministrationGridView.AllowUserToResizeRows = false;
            this.EmployeesForAdministrationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmployeesForAdministrationGridView.Location = new System.Drawing.Point(10, 10);
            this.EmployeesForAdministrationGridView.Name = "EmployeesForAdministrationGridView";
            this.EmployeesForAdministrationGridView.RowHeadersVisible = false;
            this.EmployeesForAdministrationGridView.Size = new System.Drawing.Size(600, 380);
            this.EmployeesForAdministrationGridView.TabIndex = 0;
            this.EmployeesForAdministrationGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.EmployeesForAdministrationGridView_CellParsing);
            this.EmployeesForAdministrationGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.EmployeesForAdministrationGridView_CellFormatting);
            this.EmployeesForAdministrationGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.EmployeesGridView_CellContentClick);
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(180, 400);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(160, 23);
            this.SaveChangesButton.TabIndex = 0;
            this.SaveChangesButton.Text = "Zapisz zmiany";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // Edytuj
            // 
            this.Edytuj.HeaderText = "";
            this.Edytuj.Name = "Edytuj";
            this.Edytuj.Text = "Edytuj...";
            this.Edytuj.UseColumnTextForButtonValue = true;
            this.Edytuj.Width = 60;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(350, 400);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(160, 23);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "Odśwież";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // EmployeeAdministrationPage
            // 
            this.Controls.Add(this.AddEmployeeButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.EmployeesForAdministrationGridView);
            this.Size = new System.Drawing.Size(300, 100);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.EmployeeAdministrationPage_ControlAdded);
            this.Resize += new System.EventHandler(this.EmployeeAdministrationPage_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesForAdministrationGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddEmployeeButton;
		private System.Windows.Forms.DataGridView EmployeesForAdministrationGridView;
        private System.Windows.Forms.Button SaveChangesButton;
        private System.Windows.Forms.DataGridViewButtonColumn Edytuj;
        private Button RefreshButton;


    }
}
