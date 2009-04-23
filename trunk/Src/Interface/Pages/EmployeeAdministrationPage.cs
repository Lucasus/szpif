﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;
using System.Data.SqlTypes;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace Interface
{
    [Designer(typeof(TabPage))]
    [ToolboxBitmap(typeof(TabPage))]
    public partial class EmployeeAdministrationPage : TabPage
    {
        DataTable schema;
        public EmployeeAdministrationPage(string text) : base(text)
        {
            InitializeComponent();
            schema = Program.Context.DataManager.bindToView(EmployeesForAdministrationGridView);
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
            Program.Context.ActualSchema = schema;
            Program.Context.ActualGridView = EmployeesForAdministrationGridView;
            Program.Context.FormManager.showForm("ChangeEmployeeForm");
            // Retrieve the task ID.

        }

        private void EmployeesForAdministrationGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = this.EmployeesForAdministrationGridView.Columns[e.ColumnIndex];
            if( schema.Columns.Contains(column.Name) &&
                schema.Columns[column.Name].DataType.Name == "SqlXml")
            {
                if (e.Value != null)
                {

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(new StringReader(e.Value.ToString()));

                    XElement xml = XElement.Load(new StringReader(e.Value.ToString()));
                    var query = from x in xml.Elements("Item")
                                where (int)x.Attribute("Value") == 1
                                select x;
                    string value = "";
                    foreach (var record in query)
                    {
                        value += record.Attribute("Name").Value + ", ";
                    }
                    e.Value = value;
                }
            }

        }

        private void EmployeesForAdministrationGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
//            object ob = e.Value;
            e.Value = new SqlString((string)e.Value);
//            e.DesiredType = "SqlString";
//            e.
        }
    }
}
