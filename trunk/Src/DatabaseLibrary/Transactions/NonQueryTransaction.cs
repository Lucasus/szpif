using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace DatabaseLibrary
{
    class NonQueryTransaction : SzpifTransaction
    {
        DbCommand cmd;

        public NonQueryTransaction(string command)
        {
            cmd = SzpifDatabase.Factory.CreateCommand();
            cmd.CommandText = command;
            cmd.Connection = SzpifDatabase.Connection;
        }
        protected override void execute()
        {
            cmd.ExecuteNonQuery();
        }
    }
}
