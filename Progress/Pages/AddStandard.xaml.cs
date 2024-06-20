using MySql.Data.MySqlClient;
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
    /// Логика взаимодействия для AddStandard.xaml
    /// </summary>
    public partial class AddStandard : Window
    {
        public AddStandard()
        {
            InitializeComponent();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {

            string sql1 = "Insert into standards (standard) values (@standard) ";

            MySqlCommand cmd1 = new MySqlCommand();
            cmd1.CommandText = sql1;

            cmd1.Parameters.AddWithValue("@standard", boxname.Text);

            if (new MySql.MySqlOp().SetBD(cmd1))
            {
                StandardsPage.sp.UpdateList();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
    }
}
