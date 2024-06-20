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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            MainWindow.mw.mainframe.Content = new AuthPage();
        }

        private void Classes_Button(object sender, RoutedEventArgs e)
        {
            mainframe.Content = new ClassesPage();
        }

        private void Students_Button(object sender, RoutedEventArgs e)
        {
            mainframe.Content = new StudentsPage();

        }

        private void Awards_Button(object sender, RoutedEventArgs e)
        {
            mainframe.Content = new AwardsPage();

        }

        private void Standards_Button(object sender, RoutedEventArgs e)
        {
            mainframe.Content = new StandardsPage();

        }
    }
}
