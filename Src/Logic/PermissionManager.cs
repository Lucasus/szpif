using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class PermissionManager
    {
        private Context Context;
        public PermissionManager(Context c)
        {
            Context = c;
        }

        public void tryLogin(string username, string password)
        {
            if (Context.Database.CheckLogin(username, password) == true)
            {
                Context.Database.setupConnectionParameters(username, password);
                Context.UserLogin = username;
                Context.UserPassword = password;
                Context.UserPermissions = Context.Database.getUserPermissions();
                Context.FormManager.switchForm("LoginForm","MainForm");
            }
            else
            {
                Context.FormManager.showMessageBox("Podałeś zły login i/lub hasło");
            }
        }
    }
}
