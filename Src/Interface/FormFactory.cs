using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;
using System.Windows.Forms;

namespace Interface
{
    public class FormFactory : IFormFactory
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
            }
            return null;
        }

    }
}
