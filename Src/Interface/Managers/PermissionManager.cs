using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Szpif;

namespace Szpif
{
    public class PermissionManager
    {    
        private SzpifDatabase database;

        public PermissionManager(SzpifDatabase database)
		{
			this.database = database;
		}

		public ICollection<string> getUserPermissions(string username, string password)
		{
		    if(database.CheckLogin(username, password) == true)
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
    }
}
