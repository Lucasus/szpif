using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif.Controls.ContentControls;
using System.Xml;

namespace Szpif
{
    public class InterfaceManager
    {
        ICollection<string> roles;
        public InterfaceManager()
        {
        }

        public Control buildFromNode(XmlNode node)
        {
            // przetwarzamy rekurencyjnie wszystkich synow, zbieramy te widoki, do ktorych mamy uprawnienia
            List<Control> controls = new List<Control>();
            foreach (XmlNode child in node.ChildNodes)
            {
                Control c = buildFromNode(child);
                if (c != null) controls.Add(c);
            }

            // przetwarzamy aktualny wezel
            if (controls.Count == 0)
            {
                if(roles.Contains(node.Attributes["roles"].Value.ToString()))
                {
                    string viewName = node.Attributes["viewname"].Value.ToString();
                    string name = node.Attributes["name"].Value.ToString();
                    ViewControl viewControl = new ViewControl(viewName, 450);
                    viewControl.Name = name;
                    return viewControl;
                }
                else
                    return null;
            }
            else if (controls.Count == 1)
            {
                return controls[0];
            }
            else
            {
                TabbedControl tabbedControl = new TabbedControl(controls);
                tabbedControl.Name = node.Attributes["name"].Value.ToString();
                return tabbedControl;
            }
        }

        public void buildInterface()
        {
            roles = Program.Context.UserRoles;
            Form mainForm = Program.Context.FormManager.getForm("MainForm");

            XmlTextReader reader = new XmlTextReader("..//..//interface.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            Control mainControl = buildFromNode(xmlDoc.DocumentElement);
            mainControl.Height = mainForm.Height - 15;
            mainForm.Controls.Add(mainControl);
            Program.Context.FormManager.switchForm("LoginForm", "MainForm");
        }
    }
}
