using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Szpif;

namespace Szpif
{
    public static class Program
    {
        public static Context Context;

        static Program()
        {
			Context = new Context(SzpifDatabase.DataBase, new FormManager(new FormFactory()));
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Context.FormManager.getForm("LoginForm"));
        }
    }
}
