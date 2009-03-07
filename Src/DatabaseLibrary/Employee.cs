using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    /// <summary>
    /// Klasa reprezentuje pojedynczego pracownika firmy oraz wszystkie jego dane.
    /// </summary>
    public class Employee
    {
        string login;
        string password;
        string name;
        string rank;

        Guid id;

        public Employee(string login, string password, string name, string rank)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.rank = rank;
            this.id = System.Guid.NewGuid();
        }

        public string Rank
        {
            get { return rank; }
            set { rank = value; }
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
