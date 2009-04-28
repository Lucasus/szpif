using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Logic
{
    public class PermissionManager
    {    
        private IDatabase database;
       // private DataManager dataManager;

		public PermissionManager(IDatabase database)
		{
			this.database = database;
			//this.dataManager = dataManager;
		}

		public ICollection<string> getUserPermissions(string username, string password)
		{
		    if (database.CheckLogin(username, password) == true)
            {
				database.setupConnectionParameters(username, password);
				DataManager dataManager = new DataManager(database);
				ICollection<string> roles = dataManager.getCurrentUserRoles();
				roles.Add("Ogólne");
				return roles;
			}
			else
			{
				return null;
			}
		}

        /*public void tryLogin(string username, string password)
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
        }*/
    }
}
