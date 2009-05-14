﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Szpif
{
    public partial class LinkControl : SzpifControl
    {
        int linkedRowId;

        public int LinkedRowId
        {
            get { return linkedRowId; }
            set { linkedRowId = value; }
        }

        public string LabelText
        {
            get { return this.ColumnValue.Text; }
            set { ColumnValue.Text = value; }
        }

        SzpifType linkType;

        public SzpifType LinkType
        {
            get { return linkType; }
            set { linkType = value; }
        }
        string searchViewName;
        SelectForm selectForm;
        public LinkControl(string columnName, SzpifType type) : base(columnName)
        {
            InitializeComponent();
            this.Name = columnName;
            this.linkType = type;
            this.searchViewName = type.Additional;
            this.ColumnNameLabel.Text = columnName;
            this.ColumnValue.ReadOnly = true;
            this.ColumnValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
        }


        private void SelectButton_Click(object sender, EventArgs e)
        {
            selectForm = FormFactory.createSelectForm(this);
//            selectForm = (SelectForm)Program.Context.FormManager.getForm(searchViewName + "Form");
            selectForm.ShowDialog();
/*            Program.Context.FormManager.showForm("SelectPrzelozonyForm");
            SelectPrzelozonyForm form = (SelectPrzelozonyForm)Program.Context.FormManager.getForm("SelectPrzelozonyForm");
            DataGridViewRow row = form.SelectedRow;
            foreach (Control c in valueBoxes)
            {
                if (c.Name == "Przelozony")
                {
                    LinkedTextBox box = (LinkedTextBox)c;
                    box.Text = row.Cells["Name"].Value.ToString();
                    box.RowId = Int32.Parse(row.Cells["Id"].Value.ToString());
                }
            }

            */
        }

        public override void fill(string data)  // zakładam, że daną jest string xml
        {
            //string help = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
            if (data != "")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(new StringReader(data));
                XElement xml = XElement.Load(new StringReader(data));

                this.ColumnValue.Text = xmlDoc.DocumentElement.GetAttribute("Text");
                this.linkedRowId = Int32.Parse(xmlDoc.DocumentElement.GetAttribute("Id"));
            }
    //        this.searchViewName = xmlDoc.DocumentElement.GetAttribute("ViewName");
            // TO DO: select form musi wiedzieć, jaką wartość ma zwrócić

        }

        public string getDefaultData()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootNode = xmlDoc.CreateElement("Link");
            rootNode.SetAttribute("Text", this.ColumnValue.Text);
            rootNode.SetAttribute("Id", this.linkedRowId.ToString());
            xmlDoc.AppendChild(rootNode);
            return xmlDoc.OuterXml;
        }

        public override object getData()
        {
            string help = getDefaultData();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(help));

            xmlDoc.DocumentElement.SetAttribute("Text", this.ColumnValue.Text);
            xmlDoc.DocumentElement.SetAttribute("Id", this.linkedRowId.ToString());
            if (xmlDoc.OuterXml != "")
                return xmlDoc.OuterXml;
            else return DBNull.Value;
//            SqlXml newxml = new SqlXml(new XmlTextReader(new StringReader(xmlDoc.OuterXml)));
//            gridView.Rows[row].Cells[valueBox.Name].Value = xmlDoc.OuterXml;
        }

    }
}