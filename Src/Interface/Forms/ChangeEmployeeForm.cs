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

namespace Interface
{
    public partial class ChangeEmployeeForm : Form
    {
        int row;
        DataGridView gridView;
        List<Control> valueBoxes;
        void getDataFromGrid()
        {
            row = Program.Context.ActualGridArguments.RowIndex;
            foreach (Control valueBox in valueBoxes)
            {
                if (valueBox is CheckedListBox)
                {
                    CheckedListBox box = (CheckedListBox)valueBox;
                    string help = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(new StringReader(help));
                    XElement xml = XElement.Load(new StringReader(help));
                    
                    var query = from x in xml.Elements("Item")
                                where (int)x.Attribute("Value") == 1
                                select x;

                    for (int i = 0; i < box.Items.Count; ++i)
                        box.SetItemChecked(i, false);
                    foreach (var record in query)
                        for (int i = 0; i < box.Items.Count; ++i)
                            if (box.GetItemText(box.Items[i]) == record.Attribute("Name").Value)
                                box.SetItemChecked(i, true);
                }
                else
                {
                    valueBox.Text = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
                }
            }
        }
        public ChangeEmployeeForm()
        {
            InitializeComponent();
            gridView = Program.Context.ActualGridView;
            DataTable schema = Program.Context.ActualSchema;
            valueBoxes = Program.Context.ContentManager.generateContent(this, gridView, schema);
            getDataFromGrid();
        }

        protected void MainForm_Activated(object sender, System.EventArgs e)
        {
            getDataFromGrid();
        }

        protected void MainForm_Closed(object sender, System.EventArgs e)
        {
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            foreach (Control valueBox in valueBoxes)
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
                        if(box.GetItemChecked(i))
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void deleteEmployeeButton_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gridView.DataSource;
//            dt.Rows.
            int id = (int)gridView.Rows[row].Cells["Id"].Value;
            DataRow[] dr = dt.Select("Id = " + id);
            dr[0].Delete();
//            dt.Rows[  ].Delete();
            this.Close();
        }
    }
}
