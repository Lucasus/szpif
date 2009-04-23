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

                    if (box.Items.Count == 0)
                    {
                        box.Items.AddRange(new object[] {
                            "Właściciel",
                            "Project Manager",
                            "Przełożony",
                            "Pracownik",
                            "Opiekun handlowy"});
                        box.Height = 20 * box.Items.Count;
                    }

                    for (int i = 0; i < box.Items.Count; ++i)
                    {
                        box.SetItemChecked(i, false);
                    }
                    
                    foreach (var record in query)
                    {
                        for(int i = 0 ; i < box.Items.Count ; ++i)
                        {
                            if (box.GetItemText(box.Items[i]) == record.Attribute("Name").Value)
                            {
                                box.SetItemChecked(i,true);
                            }
                        }
                    }


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
            valueBoxes = new List<Control>();
            gridView = Program.Context.ActualGridView;
            // współrzędne lewego górnego rogu.
            int x = 17;
            int y = 15;
            int height = 13;
            int width = 50;
            int space = 9;
            int counter = 0;
            DataTable schema = Program.Context.ActualSchema;
            int aktY = y;
            foreach(DataGridViewColumn column in gridView.Columns)
            {
                if (column.Name != "Edytuj" && column.Name != "Id")
                {
                    Label label = new Label();
                    label.Location = new Point(x, aktY); 
                    label.Name = column.Name + "Label";
                    label.Size = new Size(width, height);
                    label.TabIndex = counter + 1;
                    label.Text = column.Name;
                    this.Controls.Add(label);

                    if (schema.Columns.Contains(column.Name) &&
                        schema.Columns[column.Name].DataType.Name == "SqlXml")
         //               if (column.ValueType.Name == "SqlXml")
                    {
                        CheckedListBox listBoxValue = new CheckedListBox();
                        listBoxValue.Location = new Point(x + label.Width + 5, aktY);
                        listBoxValue.Name = column.Name;
                        listBoxValue.CheckOnClick = true;
                        listBoxValue.Size = new Size(width * 4, 120);

                        this.Controls.Add(listBoxValue);
                        valueBoxes.Add(listBoxValue);

                    }
                    else
                    {

                        TextBox valueBox = new TextBox();
                        valueBox.Location = new Point(x + label.Width + 5, aktY);
                        valueBox.Name = column.Name;
                        valueBox.Size = new Size(width * 4, height - 3);

                        this.Controls.Add(valueBox);
                        valueBoxes.Add(valueBox);
                        aktY += space + height;
                    }
                    ++counter;
                }
            }
            
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
                    //XElement xml = XElement.Load(new StringReader(help.Value));
//                    XmlNodeList xnList = xml.SelectNodes("/Names/Name[@type='M']");
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
    }
}
