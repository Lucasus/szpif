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
        public EmployeeAdministrationPage(string text) : base(text)
        {
            InitializeComponent();
            Program.Context.DataManager.bindToView(EmployeesForAdministrationGridView);
            this.EmployeesForAdministrationGridView.Columns.AddRange(new DataGridViewColumn[] {
            this.Edytuj});
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            Program.Context.DataManager.updateView(EmployeesForAdministrationGridView);
        }

        private void EmployeesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not on button cells. 
            if (e.RowIndex < 0 || e.ColumnIndex !=
                EmployeesForAdministrationGridView.Columns["Edytuj"].Index) return;

            //MessageBox.Show("Login: " + EmployeesGridView.Rows[e.RowIndex].Cells["Login"].Value);
            Program.Context.ActualGridArguments = e;
            Program.Context.ActualGridView = EmployeesForAdministrationGridView;
            Program.Context.FormManager.showForm("ChangeEmployeeForm");
            // Retrieve the task ID.
            //Int32 taskID = (Int32)dataGridView1[0, e.RowIndex].Value;

            // Retrieve the Employee object from the "Assigned To" cell.
            //Employee assignedTo = dataGridView1.Rows[e.RowIndex]
            //    .Cells["Assigned To"].Value as Employee;

            // Request status through the Employee object if present. 
            //if (assignedTo != null)
            //{
            //    assignedTo.RequestStatus(taskID);
            //}
            //else
           // {
            //    MessageBox.Show(String.Format(
            //        "Task {0} is unassigned.", taskID), "Status Request");
           // }

        }
    }
}
