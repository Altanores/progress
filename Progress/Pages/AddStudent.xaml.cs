using MySql.Data.MySqlClient;
using Progress.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public AddStudent()
        {
            InitializeComponent();

            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM classes";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    boxclass.Items.Add(reader.GetString(1));
                }
                reader.Close();
                conn.Close();
            }
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            int classesid = 0;
            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM classes where class=@class";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@class", boxclass.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classesid=reader.GetInt32(0);
                }
                reader.Close();
                conn.Close();
            }
            string sql1 = "Insert into students (name,classes,gender,dr) values (@name,@classes,@gender,@dr) ";

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.CommandText = sql1;

            cmd1.Parameters.AddWithValue("@name", boxname.Text);
            cmd1.Parameters.AddWithValue("@classes", classesid);
            cmd1.Parameters.AddWithValue("@gender", boxgend.Text);
            cmd1.Parameters.AddWithValue("@dr", Convert.ToDateTime(boxdr.Text));

            if (new MySql.MySqlOp().SetBD(cmd1))
            {
                StudentsPage.sp.UpdateList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
