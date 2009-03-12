namespace Interface
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
            this.EmployeesGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Permission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangePermissions = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AddEmployeeButton
            // 
            this.AddEmployeeButton.Location = new System.Drawing.Point(10, 400);
            this.AddEmployeeButton.Name = "AddEmployeeButton";
            this.AddEmployeeButton.Size = new System.Drawing.Size(75, 23);
            this.AddEmployeeButton.TabIndex = 0;
            this.AddEmployeeButton.Text = "Nowy pracownik";
            this.AddEmployeeButton.UseVisualStyleBackColor = true;
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(200, 400);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(75, 23);
            this.SaveChangesButton.TabIndex = 0;
            this.SaveChangesButton.Text = "Zapisz zmiany";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // EmployeesGridView
            // 
            this.EmployeesGridView.AllowUserToAddRows = false;
            this.EmployeesGridView.AllowUserToDeleteRows = false;
            this.EmployeesGridView.AllowUserToResizeColumns = false;
            this.EmployeesGridView.AllowUserToResizeRows = false;
            this.EmployeesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmployeesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Login,
            this.EmployeeName,
            this.Permission,
            this.ChangePermissions,
            this.Delete});
            this.EmployeesGridView.Location = new System.Drawing.Point(10, 10);
            this.EmployeesGridView.Name = "EmployeesGridView";
            this.EmployeesGridView.RowHeadersVisible = false;
            this.EmployeesGridView.Size = new System.Drawing.Size(600, 380);
            this.EmployeesGridView.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Width = 20;
            // 
            // Login
            // 
            this.Login.HeaderText = "Login";
            this.Login.Name = "Login";
            // 
            // Name
            // 
            this.EmployeeName.HeaderText = "Imię i nazwisko";
            this.EmployeeName.Name = "Name";
            this.EmployeeName.Width = 150;
            // 
            // Permission
            // 
            this.Permission.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Permission.HeaderText = "Uprawnienia";
            this.Permission.Name = "Permission";
            this.Permission.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ChangePermissions
            // 
            this.ChangePermissions.HeaderText = "";
            this.ChangePermissions.Name = "ChangePermissions";
            this.ChangePermissions.Text = "Zmień";
            this.ChangePermissions.UseColumnTextForButtonValue = true;
            this.ChangePermissions.Width = 60;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Usuń";
            this.Delete.Name = "Delete";
            this.Delete.Text = "Usuń";
            this.Delete.UseColumnTextForButtonValue = true;
            this.Delete.Width = 50;
            // 
            // EmployeeAdministrationPage
            // 
            this.Controls.Add(this.AddEmployeeButton);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.EmployeesGridView);
            ((System.ComponentModel.ISupportInitialize)(this.EmployeesGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddEmployeeButton;
        private System.Windows.Forms.DataGridView EmployeesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Permission;
        private System.Windows.Forms.DataGridViewButtonColumn ChangePermissions;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.Button SaveChangesButton;


    }
}
