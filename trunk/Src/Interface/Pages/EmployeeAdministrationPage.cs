#region usings
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;
using System.Data.SqlTypes;
using System.Xml;
using System.IO;
using System.Xml.Linq;
#endregion
namespace Szpif
{
    [Designer(typeof(TabPage))]
    [ToolboxBitmap(typeof(TabPage))]
    public partial class EmployeeAdministrationPage : TabPage
    {
        IntegratedView view;
        public EmployeeAdministrationPage(string text) : base(text)
        {
            InitializeComponent();
            //this.Width = this.Parent.Width;
            this.EmployeesForAdministrationGridView.Width = this.Width - 10;
            this.EmployeesForAdministrationGridView.Columns.AddRange(new DataGridViewColumn[] {
            this.Edytuj});
            view = Program.Context.ViewToGridManager.bindToView(EmployeesForAdministrationGridView);
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            Program.Context.ViewToGridManager.updateView(EmployeesForAdministrationGridView);
        }

        private void EmployeesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not on button cells. 
            if (e.RowIndex < 0 || e.ColumnIndex !=
                EmployeesForAdministrationGridView.Columns["Edytuj"].Index) return;

            //MessageBox.Show("Login: " + EmployeesGridView.Rows[e.RowIndex].Cells["Login"].Value);
            Program.Context.ActualGridArguments = e;
            Program.Context.ActualIntegratedView = view;
            Program.Context.ActualGridView = EmployeesForAdministrationGridView;
            Program.Context.FormManager.showForm("ChangeEmployeeForm");
            // Retrieve the task ID.

        }

        private void EmployeesForAdministrationGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = this.EmployeesForAdministrationGridView.Columns[e.ColumnIndex];
            if(view.Columns.ContainsKey(column.Name))
                e.Value = view.Columns[column.Name].valueToGridString(e.Value.ToString());            
        }

        private void EmployeesForAdministrationGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
        }

        private void EmployeeAdministrationPage_ControlAdded(object sender, ControlEventArgs e)
        {
        }

        private void EmployeeAdministrationPage_Resize(object sender, EventArgs e)
        {
            this.EmployeesForAdministrationGridView.Width = this.Width - 20;
        }

        private void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            Program.Context.ActualIntegratedView = view;
            Program.Context.ActualGridView = EmployeesForAdministrationGridView;
            Program.Context.FormManager.showForm("AddEmployeeForm");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            view = Program.Context.ViewToGridManager.reconnect(EmployeesForAdministrationGridView);

        }
    }
}
