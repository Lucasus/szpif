using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Xml;

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
        public List<Control> generateContent(Form InsertForm, DataGridView gridView, DataTable schema)
        {
            List<Control> valueBoxes = new List<Control>();
            // zmienne pomocnicze do obliczania położenia elementów.
            int x = 17;
            int y = 15;
            int height = 13;
            int width = 50;
            int space = 9;
            int counter = 0;

            int aktY = y;
            foreach (DataGridViewColumn column in gridView.Columns)
            {

                if (schema.Columns.Contains(column.Name) && column.Name != "Id")
                {
                    // tworzymy pole tekstowe opisujące pole
                    Label label = new Label();
                    label.Location = new Point(x, aktY);
                    label.Name = column.Name + "Label";
                    label.Size = new Size(width, height);
                    label.TabIndex = counter + 1;
                    label.Text = column.Name;
                    InsertForm.Controls.Add(label);
                    Control control = generateValueField(column, label, schema);
                    InsertForm.Controls.Add(control);
                    valueBoxes.Add(control);
                    aktY += space + height;
                    ++counter;
                }
            }
            return valueBoxes;        
        }

        private Control generateValueField(DataGridViewColumn column, Label label, DataTable schema)
        {
            Control control = null;
            switch (schema.Columns[column.Name].DataType.Name)
            {
                case "SqlXml":
                    {
                        CheckedListBox listBoxValue = new CheckedListBox();
                        listBoxValue.Location = new Point(label.Location.X + label.Width + 5, label.Location.Y);
                        listBoxValue.Name = column.Name;
                        listBoxValue.CheckOnClick = true;
                        listBoxValue.Size = new Size(label.Width * 4, 120);
                        control = listBoxValue;
                        if (column.Name == "Roles")
                        {
                            listBoxValue.Items.AddRange(new object[] {
                            "Właściciel",
                            "Project Manager",
                            "Przełożony",
                            "Pracownik",
                            "Opiekun handlowy"});
                            listBoxValue.Height = 20 * listBoxValue.Items.Count;

                        }
                        break;
                    }
                default:
                    {
                        TextBox valueBox = new TextBox();
                        valueBox.Location = new Point(label.Location.X + label.Width + 5, label.Location.Y);
                        valueBox.Name = column.Name;
                        valueBox.Size = new Size(label.Width * 4, 120);
                        control = valueBox;
                        break;
                    }
            }
            return control;
        }


        public void addRowToView(List<Control> valueBoxes, DataGridView gridView, DataTable schema)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            DataRow dr = dt.NewRow();
            foreach (Control control in valueBoxes)
            {
                switch (schema.Columns[control.Name].DataType.Name)
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
    }
}
