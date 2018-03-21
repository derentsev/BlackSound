using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    public class CreateDatabase
    {
        private readonly string connectionString;

        public CreateDatabase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateDB()
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
                @"";
        }
    }
}

