using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;

namespace Szpif
{
    public class FormManager
    {
        private Dictionary<string,Form> forms;
        private FormFactory formFactory;

        public FormManager(FormFactory factory)
        {
            forms = new Dictionary<string,Form>();
            formFactory = factory;
        }
        public Form getForm(string formName)
        {
            if (forms.ContainsKey(formName))
                return forms[formName];
            else
            {
                forms.Add(formName, FormFactory.createNewForm(formName));
                return forms[formName];
            }
        }

        public void showMessageBox(string text)
        {
            MessageBox.Show(text);
        }

        public void switchForm(string prevForm, string nextForm)
        {
            forms[prevForm].Hide();
            getForm(nextForm).ShowDialog();
            forms[prevForm].Dispose();
            forms[prevForm] = null;
        }

        public void showForm(string formName)
        {
            Form toShow = getForm(formName);
            toShow.ShowDialog();
        }
    }
}
