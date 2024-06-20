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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для AwardsPage.xaml
    /// </summary>
    public partial class AwardsPage : Page
    {
        public static AwardsPage aw {  get; set; }
        public AwardsPage()
        {
            InitializeComponent();
            aw = this;
            UpdateList();
        }

        public void UpdateList()
        {

            List<AwardModel> allaward = new List<AwardModel>();
            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM awards";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allaward.Add(new AwardModel() { id = reader.GetInt32(0), student=reader.GetInt32(1).ToString(),standard=reader.GetInt32(2).ToString(),comment=reader.GetString(3)});
                }
                reader.Close();
                conn.Close();
            }
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
                    foreach (var data in allaward)
                    {
                        if (data.student == reader.GetInt32(0).ToString())
                        {
                            data.student = reader.GetString(1);
                        }
                    }
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
                    foreach (var data in allaward)
                    {

                        if (data.standard == reader.GetInt32(0).ToString())
                        {
                            data.standard = reader.GetString(1);
                        }
                    }
                }
                reader.Close();
                conn.Close();
            }
            mainlist.ItemsSource = null;
            mainlist.ItemsSource = allaward;
        }
        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddAward().Show();
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                AwardModel data = (AwardModel)mainlist.SelectedItem;
                if (data != null)
                {
                    new EditAward(data).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                AwardModel data = (AwardModel)mainlist.SelectedItem;
                if (data != null)
                {
                    string sql = $"Delete from awards where id = @id";
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
