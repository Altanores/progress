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
    /// Логика взаимодействия для StandardsPage.xaml
    /// </summary>
    public partial class StandardsPage : Page
    {
        public static StandardsPage sp {  get; set; }
        public StandardsPage()
        {
            InitializeComponent();
            sp = this;
            UpdateList();
        }

        public void UpdateList()
        {

            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                List<StandardModel> allclasses = new List<StandardModel>();
                conn.Open();
                string sql = "SELECT * FROM standards";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allclasses.Add(new StandardModel() { id = reader.GetInt32(0), standard = reader.GetString(1) });
                }
                reader.Close();
                conn.Close();
                mainlist.ItemsSource = null;
                mainlist.ItemsSource = allclasses;
            }
        }
        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddStandard().Show();
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                StandardModel data = (StandardModel)mainlist.SelectedItem;
                if (data != null)
                {
                    new EditStandard(data).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                StandardModel data = (StandardModel)mainlist.SelectedItem;
                if (data != null)
                {
                    string sql = $"Delete from standards where id = @id";
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
