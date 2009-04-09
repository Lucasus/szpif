using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
                Context.UserRoles = 
                    Context.DataManager.getColumnValuesFromView("RolesViewForCurrentUser", "Role");
                Context.UserRoles.Add("Ogólne");
                Context.FormManager.switchForm("LoginForm","MainForm");
            }
            else
            {
                Context.FormManager.showMessageBox("Podałeś zły login i/lub hasło");
            }
        }
    }
}
