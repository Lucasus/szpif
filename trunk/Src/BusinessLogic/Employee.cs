using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Employee
    {
        string login;
        string password;
        string name;
        Guid id;

        public Employee(string login, string password, string name, Guid id)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.id = id;
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
