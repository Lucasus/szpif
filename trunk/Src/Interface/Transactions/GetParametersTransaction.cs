using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Szpif
{
    public class GetParametersTransaction : SzpifTransaction
    {
        string viewName;
        SqlCommand cmd;
        List<string> parameters;
        public List<string> Parameters
        {
            get { return parameters; }
        }
        public GetParametersTransaction(string viewName, string functionType)
        {
            this.parameters = new List<string>();
            this.viewName = viewName;
            cmd = new SqlCommand();
            cmd.Connection = (SqlConnection)SzpifDatabase.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = functionType + viewName;
        }

        protected override void execute()
        {
            SqlCommandBuilder.DeriveParameters(cmd);
            foreach (SqlParameter param in cmd.Parameters)
            {
                if (param.ParameterName != "@RETURN_RESULT")
                    parameters.Add(param.ParameterName.Substring(1));
//                param.SourceColumn = param.ParameterName.Substring(1);
            }
        }

    }
}
