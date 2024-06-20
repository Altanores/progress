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
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace Progress.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditClasses.xaml
    /// </summary>
    public partial class EditClasses : Window
    {
        private ClassesModel dt {  get; set; }
        public EditClasses(ClassesModel data)
        {
            InitializeComponent();
            dt = data;
            boxclass.Text = dt.classes;
            boxteacher.Text = dt.teacher;
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            string sql = "Update classes set class=@class,teacher=@teacher where id=@id";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@id", dt.id);
            cmd.Parameters.AddWithValue("@class", boxclass.Text);
            cmd.Parameters.AddWithValue("@teacher", boxteacher.Text);
            // Выполнить Command (использованная для  delete, insert, update).

            if (new MySql.MySqlOp().SetBD(cmd))
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
