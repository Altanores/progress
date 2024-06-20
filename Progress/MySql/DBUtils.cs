using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.MySql
{
    public class DBUtils
    {
        private static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }

        public static MySqlConnection SetDBConnection()
        {
            string host = "185.173.93.211";
            int port = 3306;
            string database = "serverbd";
            string username = "root";
            string password = "555812";

            return GetDBConnection(host, port, database, username, password);
        }
    }
}
