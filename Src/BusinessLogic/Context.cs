using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Context
    {
		private static Context currentContext;
        private string currentUserLogin;
        private string currentUserPassword;
        private ICollection<string> currentPermissions;

		private Context(string login, string password, ICollection<string> perm)
        {
            this.currentUserLogin = login;
            this.currentUserPassword = password;
            this.currentPermissions = perm;
        }
        
        public static void initialize(string login, string password, ICollection<string> permissions)
        {
			if(currentContext == null)
			{
				currentContext = new Context(login, password, permissions);
			}
        }
        
        public static Context CurrentContext
        {
			get { return currentContext; }
        }
		
        public string CurrentUserLogin
        {
            get { return currentUserLogin; }
            set { currentUserLogin = value; }
        }

        public string CurrentUserPassword
        {
            get { return currentUserPassword; }
            set { currentUserPassword = value; }
        }

        public ICollection<string> CurrentPermissions
        {
            get { return currentPermissions; }
            set { currentPermissions = value; }
        }


    }
}
