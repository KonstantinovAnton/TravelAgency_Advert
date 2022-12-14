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

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Base.EM = new Entities4();
            frm.Navigate(new PageAuthorization());


        }

        private void gotoPageRegistration_Click(object sender, RoutedEventArgs e)
        {
            frm.Navigate(new PageRegistration());
        }

        private void gotoPageАuthorization_Click(object sender, RoutedEventArgs e)
        {
            frm.Navigate(new PageAuthorization());
        }

        private void gotoPageAdvert_Click(object sender, RoutedEventArgs e)
        {
            frm.Navigate(new PageAdvert());
        }
    }
}
