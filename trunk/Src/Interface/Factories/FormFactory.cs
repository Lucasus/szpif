using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szpif;
using System.Windows.Forms;
using Szpif.Controls.ContentControls;
using Szpif.Forms;
using Szpif.Controls.ColumnControls;
using Szpif.Forms.ContentForms;

namespace Szpif
{
    public class FormFactory
    {

        public static Form createNewForm(string kind)
        {
            switch (kind)
            {
                case "LoginForm":
                    return new LoginForm();
                case "MainForm":
                    return new MainForm();
            }
            return null;
        }

        public static Form createViewForm(string viewName, ViewControl viewControl)
        {
            Form viewForm = new Form();
            viewForm.Controls.Add(viewControl);
            viewForm.Size = viewControl.Size;
            return viewForm;
        }

        public static AddForm createAddForm(string viewName, ViewControl viewControl)
        {
            return new AddForm(viewName, viewControl);
        }

        public static UpdateForm createUpdateForm(string viewName, ViewControl viewControl)
        {
            return new UpdateForm(viewName, viewControl);
        }

        public static SelectForm createSelectForm(LinkControl linkedControl)
        {
            return new SelectForm(linkedControl);
        }

        public static CalendarForm createCalendarForm(DateTimeControl dateTimeControl, DateTime current)
        {
            return new CalendarForm(current);
        }
    }
}
