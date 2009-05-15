using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif.Forms.ContentForms;

namespace Szpif.Controls.ColumnControls
{
    public partial class DateTimeControl : SzpifControl
    {
        CalendarForm calendarForm;
        string date;
        DateTime dateTime;
        public DateTimeControl(string columnName) : base(columnName)
        {
            InitializeComponent();
            this.Name = columnName;
            this.ColumnNameLabel.Text = columnName;
        }

        public override void fill(string data)
        {
            DateTime d = DateTime.ParseExact(data, "yyyy-MM-dd HH:mm:ss", null);
            this.dateTime = d;
            this.date = d.ToLongDateString();
            this.ColumnValue.Text = d.ToLongDateString();
        }

        public override object getData()
        {
            if (this.date!= "")
                return this.date;
            else return DBNull.Value;
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.date = e.End.ToLongTimeString();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            calendarForm = FormFactory.createCalendarForm(this,dateTime);
            calendarForm.ShowDialog();
            this.date = calendarForm.Date;
            this.dateTime = calendarForm.Current;
            this.ColumnValue.Text = this.date;
        }
    }
}
