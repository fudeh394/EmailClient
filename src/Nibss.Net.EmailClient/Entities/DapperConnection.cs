using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibss.Net.EmailClient.Entities
{
   

    public class DapperConnection
    {
        public string ConnectionString;
        public DapperConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public IDbConnection DapperCon
        {
            get
            {
                return new SqlConnection(ConnectionString);

            }
        }
    }
}
