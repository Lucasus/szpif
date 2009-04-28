using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using Logic;

namespace Interface
{
    public partial class ChangeEmployeeForm : Form
    {
        int row;
        ContentManager contentManager;
        DataGridView gridView;
        List<Control> valueBoxes;

        public ChangeEmployeeForm()
        {
            row = Program.Context.ActualGridArguments.RowIndex;
            contentManager = Program.Context.ContentManager;
            InitializeComponent();
            gridView = Program.Context.ActualGridView;
            valueBoxes = contentManager.generateContent(this, gridView, Program.Context.ActualIntegratedView);
            contentManager.getDataFromGrid(valueBoxes, gridView, Program.Context.ActualIntegratedView, row);
        }

        protected void MainForm_Activated(object sender, System.EventArgs e)
        {
            row = Program.Context.ActualGridArguments.RowIndex;
            contentManager.getDataFromGrid(valueBoxes, gridView, Program.Context.ActualIntegratedView, row);
        }

        protected void MainForm_Closed(object sender, System.EventArgs e)
        {
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            foreach (Control valueBox in valueBoxes)
            {
                if (gridView.Columns.Contains(valueBox.Name))
                {

                    if (valueBox is CheckedListBox)
                    {
                        CheckedListBox box = (CheckedListBox)valueBox;

                        string help = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(new StringReader(help));
                        for (int i = 0; i < box.Items.Count; ++i)
                        {
                            string path = "/CheckedListBox/Item[@Name='" + box.GetItemText(box.Items[i]) + "']";
                            xmlDoc.SelectNodes(path);
                            XmlNodeList node = xmlDoc.SelectNodes(path);
                            if (box.GetItemChecked(i))
                                node[0].Attributes["Value"].Value = "1";
                            else
                                node[0].Attributes["Value"].Value = "0";
                        }
                        SqlXml newxml = new SqlXml(new XmlTextReader(new StringReader(xmlDoc.OuterXml))); //  StringReader(xmlDoc.OuterXml));
                        gridView.Rows[row].Cells[valueBox.Name].Value = xmlDoc.OuterXml;// xmlDoc.  new SqlXml(  xmlDoc.OuterXml;
                    }
                    else
                        gridView.Rows[row].Cells[valueBox.Name].Value = valueBox.Text;
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void deleteEmployeeButton_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            //gridView.Rows.Remove(gridView.Rows[row]);
            int id = (int)gridView.Rows[row].Cells["Id"].Value;
            DataRow[] dr = dt.Select("Id = " + id);
            dr[0].Delete();
            this.Close();
        }
    }
}
