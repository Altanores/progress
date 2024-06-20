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
    /// Логика взаимодействия для EditAward.xaml
    /// </summary>
    public partial class EditAward : Window
    {
        private AwardModel dt {  get; set; }
        public EditAward(AwardModel data)
        {
            InitializeComponent();
            dt = data;
            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM students";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    boxstud.Items.Add(reader.GetString(1));
                }
                reader.Close();
                conn.Close();
            }
            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM standards";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    boxstandard.Items.Add(reader.GetString(1));

                }
                reader.Close();
                conn.Close();
            }
            boxstud.Text = dt.student;
            boxstandard.Text = dt.standard;
            boxcomment.Text = dt.comment;
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {


            int studid = 0;
            int standid = 0;
            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM students where name=@name";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@name", boxstud.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studid = reader.GetInt32(0);
                }
                reader.Close();
                conn.Close();
            }
            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM standards where standard=@standard";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@standard", boxstandard.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    standid = reader.GetInt32(0);
                }
                reader.Close();
                conn.Close();
            }
            string sql1 = "Update awards set student=@student,standard=@standard,comment=@comment where id=@id";

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.CommandText = sql1;

            cmd1.Parameters.AddWithValue("@id", dt.id);
            cmd1.Parameters.AddWithValue("@student", studid);
            cmd1.Parameters.AddWithValue("@standard", standid);
            cmd1.Parameters.AddWithValue("@comment", boxcomment.Text);
            // Выполнить Command (использованная для  delete, insert, update).

            if (new MySql.MySqlOp().SetBD(cmd1))
            {
                AwardsPage.aw.UpdateList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
