using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Szpif.Forms.ContentForms
{
    public partial class CalendarForm : Form
    {
        string date;
        DateTime current;

        public DateTime Current
        {
            get { return current; }
            set { current = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public CalendarForm(DateTime currentDate)
        {
            InitializeComponent();
            current = currentDate;
            this.monthCalendar.BoldedDates = new DateTime[] { current };
//            this.FormBorderStyle = FormBorderStyle.None; // =  FormsBorderStyle.None;        }
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.date = e.End.ToLongDateString();
            this.Close();
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.date = e.End.ToLongDateString();
            this.current = e.End;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.date = current.ToLongDateString();
            this.Close();
        }
    }
}
