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
    /// Логика взаимодействия для EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window
    {
        private StudentsModel dt {  get; set; }
        public EditStudent(StudentsModel data)
        {
            InitializeComponent();
            dt= data;
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
            boxname.Text = dt.name;
            boxclass.Text = dt.classes;
            boxgend.Text = dt.gender;
            boxdr.Text = dt.dr;
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {

            string sql = "Update students set name=@name,classes=@classes,gender=@gender,dr=@dr where id=@id";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@id", dt.id);
            cmd.Parameters.AddWithValue("@name", boxname.Text);

            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql1 = "SELECT * FROM classes";
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = sql1;

                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    if(boxclass.Text == reader.GetString(1)) cmd.Parameters.AddWithValue("@classes", reader.GetInt32(0));
                }
                reader.Close();
                conn.Close();
            }
            cmd.Parameters.AddWithValue("@gender", boxgend.Text);
            cmd.Parameters.AddWithValue("@dr", Convert.ToDateTime(boxdr.Text));
            // Выполнить Command (использованная для  delete, insert, update).

            if (new MySql.MySqlOp().SetBD(cmd))
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
