using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlTypes;



namespace Szpif
{
    public partial class CheckedListBoxControl : SzpifControl
    {
        public CheckedListBoxControl(string columnName, SzpifType type) : base(columnName)
        {
            InitializeComponent();
            this.Name = columnName;
            this.columnValue.CheckOnClick = true;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(type.Schema));
            XElement xml = XElement.Load(new StringReader(type.Schema));

            var query = from x in xml.Elements("Item")
                        select x;

            columnValue.Height = 10;
            foreach (var record in query)
            {
                columnValue.Items.Add(record.Attribute("Name").Value);
                columnValue.Height += 15;
            }
            this.Height = columnValue.Height + 3;
            this.ColumnNameLabel.Text = columnName;
        }

        public string getDefaultValue()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootNode = xmlDoc.CreateElement("CheckedListBox");
            rootNode.SetAttribute("Name", this.Name);
            xmlDoc.AppendChild(rootNode);

            for (int i = 0; i < columnValue.Items.Count; ++i)
            {
                XmlElement itemNode = xmlDoc.CreateElement("Item");
                itemNode.SetAttribute("Name", columnValue.GetItemText(columnValue.Items[i]));
                string itemValue = "0";
                itemNode.SetAttribute("Value", itemValue);
                rootNode.AppendChild(itemNode);
            }
            return xmlDoc.OuterXml;
        }
        public override void fill(string data)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(data));
            XElement xml = XElement.Load(new StringReader(data));

            var query = from x in xml.Elements("Item")
                        where (int)x.Attribute("Value") == 1
                        select x;

            for (int i = 0; i < columnValue.Items.Count; ++i)
                columnValue.SetItemChecked(i, false);
            foreach (var record in query)
                for (int i = 0; i < columnValue.Items.Count; ++i)
                    if (columnValue.GetItemText(columnValue.Items[i]) == record.Attribute("Name").Value)
                        columnValue.SetItemChecked(i, true);
        }

        public override object getData()
        {
            string help = getDefaultValue();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(help));
            for (int i = 0; i < columnValue.Items.Count; ++i)
            {
                string path = "/CheckedListBox/Item[@Name='" +
                    columnValue.GetItemText(columnValue.Items[i]) + "']";
                xmlDoc.SelectNodes(path);
                XmlNodeList node = xmlDoc.SelectNodes(path);
                if (columnValue.GetItemChecked(i))
                    node[0].Attributes["Value"].Value = "1";
                else
                    node[0].Attributes["Value"].Value = "0";
            }
//            SqlXml newxml = new SqlXml(new XmlTextReader(new StringReader(xmlDoc.OuterXml))); //  StringReader(xmlDoc.OuterXml));
 //           gridView.Rows[row].Cells[valueBox.Name].Value = xmlDoc.OuterXml;// xmlDoc.  new SqlXml(  xmlDoc.OuterXml;*/
            if(xmlDoc.OuterXml != "")
                return xmlDoc.OuterXml;
            else 
                return DBNull.Value;
        }

    }
}
