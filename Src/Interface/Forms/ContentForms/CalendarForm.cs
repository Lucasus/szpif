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

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public CalendarForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // =  FormsBorderStyle.None;        }
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.date = e.End.ToLongDateString();
            this.Close();
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.date = e.End.ToLongDateString();
            this.Close();
        }
    }
}
