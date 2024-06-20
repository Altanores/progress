using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progress.MySql
{
    public class MySqlOp
    {
        public bool SetBD(MySqlCommand cmd)
        {
            try
            {

                MySqlConnection connection = DBUtils.SetDBConnection();
                connection.Open();                                                                  // insert "Insert into staff (name, tg, num, post, pass,branch) values (@name, @tg,@telefon,@post,@pass,@branch) "
                cmd.Connection = connection;                                                  // update "Update staff set name=@name, tg=@tg, post=@post, pass=@pass,branch=@branch where num = @telefon"
                if (connection.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Ошибка MySql");
                    return false;
                }
                else if (connection.State == ConnectionState.Open)
                {
                    // Выполнить Command (использованная для  delete, insert, update).                  // delete "Delete from staff where num = @telefon "
                    int rowCount = cmd.ExecuteNonQuery();

                    connection.Close();
                    connection.Dispose();
                    connection = null;
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
