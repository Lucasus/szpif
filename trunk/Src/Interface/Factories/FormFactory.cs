using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szpif;
using System.Windows.Forms;
using Szpif.Controls.ContentControls;
using Szpif.Forms;

namespace Szpif
{
    public class FormFactory
    {

        public Form createNewForm(string kind)
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
            ViewControl control = new ViewControl(viewName);
            viewForm.Controls.Add(control);
            viewForm.Size = control.Size;
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
    }
}
