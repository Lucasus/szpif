using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;

namespace Interface
{
    [Designer(typeof(TabPage))]
    [ToolboxBitmap(typeof(TabPage))]
    public partial class EmployeeAdministrationPage : TabPage
    {
        private SzpifDatabase database;
        public EmployeeAdministrationPage(string text) : base(text)
        {
            database = new SzpifDatabase();
            InitializeComponent();
            DataTable Employees = database.getEmployeesAdministrationView();
            // Turn this off so column names do not come from data source
            EmployeesGridView.AutoGenerateColumns = false;
            EmployeesGridView.DataSource = Employees;
            // Specify table as data source
           // EmployeesGridView.DataMember = "EmployeeAdministrationView";  // Table in dataset
            // Tie the columns in the grid to column names in the data table
            EmployeesGridView.Columns[0].DataPropertyName = "Id";
            EmployeesGridView.Columns[1].DataPropertyName = "Login";
            EmployeesGridView.Columns[2].DataPropertyName = "Name";
            EmployeesGridView.Columns[3].DataPropertyName = "Uprawnienia";
        }
    }
}
