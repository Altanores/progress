using MySql.Data.MySqlClient;
using Progress.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        public static ClassesPage cp {  get; set; }
        public ClassesPage()
        {
            InitializeComponent();
            cp = this;
            UpdateList();
        }
        public void UpdateList()
        {

            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                List<ClassesModel> allclasses = new List<ClassesModel>();
                conn.Open();
                string sql = "SELECT * FROM classes";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allclasses.Add(new ClassesModel() {id=reader.GetInt32(0), classes=reader.GetString(1),teacher=reader.GetString(2)});
                }
                reader.Close();
                conn.Close();
                mainlist.ItemsSource = null;
                mainlist.ItemsSource = allclasses;
            }
        }
        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddClasses().Show();
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                ClassesModel data = (ClassesModel)mainlist.SelectedItem;
                if (data != null)
                {
                    new EditClasses(data).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                ClassesModel data = (ClassesModel)mainlist.SelectedItem;
                if (data != null)
                {
                    string sql = $"Delete from classes where id = @id";
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
