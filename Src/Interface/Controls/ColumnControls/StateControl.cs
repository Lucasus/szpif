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

namespace Szpif
{
    public partial class StateControl : SzpifControl
    {
       SzpifType type;
       string data;
       public StateControl(string columnName, SzpifType type) : base(columnName)
        {
            this.type = type;
            InitializeComponent();
            this.Name = columnName;
            this.ColumnNameLabel.Text = columnName;
        }

        public string getDefaultValue()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(type.Schema));
            XElement xml = XElement.Load(new StringReader(type.Schema));

            string def = xmlDoc.DocumentElement.ChildNodes[0].Attributes["name"].Value;
            return def;
            
        }
        public override void fill(string data)
        {
            this.data = data;
            this.columnValue.Items.Clear();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(type.Schema));
            XElement xml = XElement.Load(new StringReader(type.Schema));

            XmlNode akt = null;
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if (node.Attributes["name"].Value == data)
                {
                    akt = node;
                }
            }

            foreach (XmlNode node in akt.ChildNodes)
            {
                this.columnValue.Items.Add(node.Attributes["name"].Value);
            }
        }

        public override object getData()
        {
            if (columnValue.SelectedItem == null) 
                return this.data;
            else
                return columnValue.SelectedItem.ToString();
        }
    }
}
