using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace DatabaseLibrary
{
    class QueryTransaction : ITransaction
    {
        DbCommand cmd;
        DbDataReader dr;

        public DbDataReader Table
        {
            get { return dr; }
        }
        
        public QueryTransaction(string command)
        {
            cmd = SzpifDatabase.Factory.CreateCommand();
            cmd.CommandText = command;
            cmd.Connection = SzpifDatabase.Connection;
        }
        protected override void execute()
        {
            dr = cmd.ExecuteReader();
        }
    }
}
