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
    /// Логика взаимодействия для EditStandard.xaml
    /// </summary>
    public partial class EditStandard : Window
    {
        private StandardModel dt {  get; set; }
        public EditStandard(StandardModel data)
        {
            InitializeComponent();
            dt = data;
            boxname.Text = data.standard;
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {

            string sql = "Update standards set standard=@standard where id=@id";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@id", dt.id);
            cmd.Parameters.AddWithValue("@standard", boxname.Text);
            // Выполнить Command (использованная для  delete, insert, update).

            if (new MySql.MySqlOp().SetBD(cmd))
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
