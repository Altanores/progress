using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClasses.xaml
    /// </summary>
    public partial class AddClasses : Window
    {
        public AddClasses()
        {
            InitializeComponent();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            string sql1 = "Insert into classes (class,teacher) values (@class,@teacher) ";

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.CommandText = sql1;

            cmd1.Parameters.AddWithValue("@class", boxclass.Text);
            cmd1.Parameters.AddWithValue("@teacher", boxteacher.Text);

            if (new MySql.MySqlOp().SetBD(cmd1))
            {
                ClassesPage.cp.UpdateList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
