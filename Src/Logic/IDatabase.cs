using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Logic
{
    public interface IDatabase
    {
        bool      CheckLogin(string login, string password);
		void      ChangePassword(string login, string password, string newPassword);
        void      setupConnectionParameters(string username, string password);

        IntegratedView getView(string viewName);

        void updateView(string viewName, DataTable viewTable);
        List<string> getWriteableAttributes(string viewName);
    }
}
