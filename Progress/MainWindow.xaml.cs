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

namespace Progress
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mw { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mw = this;
            mainframe.Content = new Pages.AuthPage();
        }
        private void Min_Button(object sender, RoutedEventArgs e)
        {

            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
