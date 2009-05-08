using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Szpif
{
    class LinkColumn : SzpifColumn
    {
        public override SzpifControl createControl()
        {
            return ControlFactory.createSzpifControl("LinkControl", this.Name, this.Type);
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
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (!CanView)
                column.Visible = false;
            return column;
        }

        public override string valueToGridString(string value)
        {
            if (value == "") return "";
            StringReader reader = new StringReader(value);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            return xmlDoc.DocumentElement.GetAttribute("Text");
        }

    }
}
