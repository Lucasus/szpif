using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Logic;
using DatabaseLibrary;

namespace Interface
{
    public static class Program
    {
        public static Context Context;
        public static FormManager FormManager;
        public static PermissionManager PermissionManager;

        static Program()
        {
            Context = new Context();
            FormManager = new FormManager(new FormFactory());
            PermissionManager = new PermissionManager(Context);
            Context.Database = SzpifDatabase.DataBase;
            Context.FormManager = FormManager;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(FormManager.getForm("LoginForm"));
        }
    }
}
