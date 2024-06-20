using MySql.Data.MySqlClient;
using Progress.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        public static StudentsPage sp {  get; set; }
        public StudentsPage()
        {
            InitializeComponent();
            sp = this;
            UpdateList();
        }
        public void UpdateList()
        {

            List<StudentsModel> allstud = new List<StudentsModel>();
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
                    allstud.Add(new StudentsModel() { id = reader.GetInt32(0), name=reader.GetString(1), classes = reader.GetInt32(2).ToString(), gender =reader.GetString(3),dr=reader.GetDateTime(4).ToString("d")});
                }
                reader.Close();
                conn.Close();
            }
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
                    foreach(var data in allstud){
                        if (data.classes == reader.GetInt32(0).ToString())
                        {
                            data.classes = reader.GetString(1);
                        }
                    }
                }
                reader.Close();
                conn.Close();
            }
            mainlist.ItemsSource = null;
            mainlist.ItemsSource = allstud;
        }
        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddStudent().Show();
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                StudentsModel data = (StudentsModel)mainlist.SelectedItem;
                if (data != null)
                {
                    new EditStudent(data).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                StudentsModel data = (StudentsModel)mainlist.SelectedItem;
                if (data != null)
                {
                    string sql = $"Delete from students where id = @id";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@id", data.id);

                    if (new MySql.MySqlOp().SetBD(cmd))
                    {
                        UpdateList();
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }
    }
}
