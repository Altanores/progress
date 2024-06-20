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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void Auth_Button(object sender, RoutedEventArgs e)
        {

            using (MySqlConnection conn = MySql.DBUtils.SetDBConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM users where login=@login";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@login", loginbox.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (passwordbox.Text == reader.GetString(2))
                    {
                        MainWindow.mw.mainframe.Content = new AdminPage();
                        return;

                    }
                    else
                    {
                        MessageBox.Show("Не верный логин и пароль");
                        return;
                    }
                }
                reader.Close();
                conn.Close();
            }
            MessageBox.Show("Не верный логин и пароль");
        }
    }
}
