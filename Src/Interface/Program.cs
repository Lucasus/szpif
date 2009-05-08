using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Szpif;

namespace Interface
{
    public static class Program
    {
        public static Context Context;
        //public static FormManager FormManager;
        //public static PermissionManager PermissionManager;
        //public static DataManager DataManager;

        static Program()
        {
			Context = new Context(SzpifDatabase.DataBase, new FormManager(new FormFactory()));
            //PermissionManager = new PermissionManager(Context);
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
