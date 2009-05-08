using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Szpif
{
    class CheckedListBoxColumn : SzpifColumn
    {
        public override SzpifControl createControl()
        {
            return ControlFactory.createSzpifControl("CheckedListBoxControl", this.Name, this.Type);
        }

        public override DataGridViewColumn createDataGridViewColumn()
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = this.Name;
            column.DataPropertyName = this.Name;
            column.ReadOnly = true;
            DataGridViewCellStyle helpStyle = new DataGridViewCellStyle();
            helpStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            column.DefaultCellStyle = helpStyle;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (!CanView)
                column.Visible = false;
            return column;
        }
        public override string valueToGridString(string value)
        {
            StringReader reader = new StringReader(value);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            XElement xml = XElement.Load(new StringReader(value));

            var query = from x in xml.Elements("Item")
                        where (int)x.Attribute("Value") == 1
                        select x;
            string newValue = "";
            foreach (var record in query)
                newValue += record.Attribute("Name").Value + ", ";
            return newValue;
        }

    }
}
