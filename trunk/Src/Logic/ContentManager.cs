using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Logic
{
    public class ContentManager
    {

        /// <summary>
        /// Generates the content for insert Form.
        /// </summary>
        /// <param name="InsertForm">The insert form.</param>
        /// <param name="grid">Grid, którego kolumny będziemy podpinać.</param>
        /// <param name="schema">Schemat widoku, który jest podpięty do datagridu.</param>
        public List<Control> generateContent(Control InsertForm, DataGridView gridView, IntegratedView view)
        {
            List<Control> valueBoxes = new List<Control>();
            // zmienne pomocnicze do obliczania położenia elementów.
            int x = 17;
            int y = 15;
            int height = 13;
            int width = 70;
            int space = 9;
            int counter = 0;

            int aktY = y;
            foreach (SqlParameter column in view.CanUpdate)
            {
                string name = column.ParameterName.Substring(1);

                if (view.CanUpdate.Contains(column.ParameterName) && name != "Id" && name != "RETURN_VALUE")
                {
                    // tworzymy pole tekstowe opisujące pole
                    Label label = new Label();
                    label.Location = new Point(x, aktY);
                    label.Name = name + "Label";
                    label.Size = new Size(width, height);
                    label.TabIndex = counter + 1;
                    label.Text = name;
                    InsertForm.Controls.Add(label);
                    Control control = generateValueField(InsertForm, column, label, view);
                    valueBoxes.Add(control);
                    aktY += space + height;
                    ++counter;
                }
            }
            return valueBoxes;        
        }

        private Control generateValueField(Control parent, SqlParameter column, Label label, IntegratedView view)
        {
            string name = column.ParameterName.Substring(1);
            Control control = null;
            switch (view.CanUpdate[column.ParameterName].DbType.ToString())
            {
                case "Xml":
                    {
                        if (name == "Roles")
                        {
                            CheckedListBox listBoxValue = new CheckedListBox();
                            listBoxValue.Location = new Point(label.Location.X + label.Width + 5, label.Location.Y);
                            listBoxValue.Name = name;
                            listBoxValue.CheckOnClick = true;
                            listBoxValue.Size = new Size(label.Width * 4, 120);
                            control = listBoxValue;
                                listBoxValue.Items.AddRange(new object[] {
                            "Właściciel",
                            "Project Manager",
                            "Przełożony",
                            "Pracownik",
                            "Opiekun handlowy"});
                                listBoxValue.Height = 20 * listBoxValue.Items.Count;
                                parent.Controls.Add(control);


                        }
                        else if (name == "Przelozony")
                        {
                            LinkedTextBox ltextBox = new LinkedTextBox();
                            ltextBox.Location = new Point(label.Location.X + label.Width + 5, label.Location.Y);
                            ltextBox.Name = name;
                            ltextBox.Size = new Size(label.Width * 4 - 30 , 120);
                            control = ltextBox;
                            parent.Controls.Add(control);
                            Button searchButton = new Button();
                            searchButton.Text = "...";
                            searchButton.Name = "searchButton";
                            searchButton.Location = new Point(ltextBox.Location.X + label.Width * 4 - 30, ltextBox.Location.Y);
                            searchButton.Size = new Size(30, 20);
                            parent.Controls.Add(searchButton);
                        }
                        break;
                    }
                default:
                    {
                        TextBox valueBox = new TextBox();
                        valueBox.Location = new Point(label.Location.X + label.Width + 5, label.Location.Y);
                        valueBox.Name = name;
                        valueBox.Size = new Size(label.Width * 4, 120);
                        control = valueBox;
                        parent.Controls.Add(control);
                        break;
                    }
            }
            return control;
        }


        public void addRowToView(List<Control> valueBoxes, DataGridView gridView, IntegratedView view)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            DataRow dr = dt.NewRow();
            foreach (Control control in valueBoxes)
            {
                switch (view.VisibleColumns[control.Name].DataType.Name)
                {
                    case "SqlXml":
                        {
                            CheckedListBox box = (CheckedListBox)control;
                            XmlDocument xmlDoc = new XmlDocument();
                            XmlElement rootNode = xmlDoc.CreateElement("CheckedListBox");
                            rootNode.SetAttribute("Name", control.Name);
                            xmlDoc.AppendChild(rootNode);

                            for (int i = 0; i < box.Items.Count; ++i)
                            {
                                XmlElement itemNode = xmlDoc.CreateElement("Item");
                                itemNode.SetAttribute("Name", box.GetItemText(box.Items[i]));
                                string itemValue;
                                if (box.GetItemChecked(i))
                                    itemValue = "1";
                                else
                                    itemValue = "0";
                                itemNode.SetAttribute("Value", itemValue);
                                rootNode.AppendChild(itemNode);
                            }

                            dr[control.Name] = xmlDoc.OuterXml;
                            break;
                        }
                    default:
                        {
                            dr[control.Name] = control.Text;
                            break;
                        }
                }

            }
            int last = (int)dt.Rows[dt.Rows.Count - 1]["Id"] + 1;
            dr["Id"] = last;
            dt.Rows.Add(dr);
        }

        public void setDataInGrid(List<Control> valueBoxes, DataGridView gridView, IntegratedView view, int row)
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
                    else if (valueBox is LinkedTextBox)
                    {
                        LinkedTextBox box = (LinkedTextBox)valueBox;

                        string help = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(new StringReader(help));

                        xmlDoc.DocumentElement.SetAttribute("Text", box.Text);
                        xmlDoc.DocumentElement.SetAttribute("Id", box.RowId.ToString());

                        SqlXml newxml = new SqlXml(new XmlTextReader(new StringReader(xmlDoc.OuterXml))); 
                        gridView.Rows[row].Cells[valueBox.Name].Value = xmlDoc.OuterXml;

                    }
                    else
                        gridView.Rows[row].Cells[valueBox.Name].Value = valueBox.Text;
                }
            }
        }
        public void getDataFromGrid(List<Control> valueBoxes, DataGridView gridView, IntegratedView view, int row)
        {

            foreach (Control valueBox in valueBoxes)
            {
                if(gridView.Columns.Contains(valueBox.Name) && gridView.Columns[valueBox.Name].Visible == true)
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
                    else if (valueBox is LinkedTextBox)
                    {
                        LinkedTextBox box = (LinkedTextBox)valueBox;
                        string help = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(new StringReader(help));
                        XElement xml = XElement.Load(new StringReader(help));

                        box.Text = xmlDoc.DocumentElement.GetAttribute("Text");
                        box.RowId = Int32.Parse(xmlDoc.DocumentElement.GetAttribute("Id"));
                    }
                    else
                    {
                        valueBox.Text = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
                    }
                }
            }
        }
    }
}
