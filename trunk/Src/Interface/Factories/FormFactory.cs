using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szpif;
using System.Windows.Forms;

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
                case "ChangeEmployeeForm":
                    return new ChangeEmployeeForm();
                case "AddEmployeeForm":
                    return new AddEmployeeForm();
                case "SelectPrzelozonyForm":
                    return new SelectPrzelozonyForm();
            }
            return null;
        }

    }
}
