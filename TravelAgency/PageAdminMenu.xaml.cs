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
    /// Логика взаимодействия для PageAdminMenu.xaml
    /// </summary>
    public partial class PageAdminMenu : Page
    {
        public PageAdminMenu()
        {
            InitializeComponent();
        }

        private void gotoPageAdminShowData_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminShowData());
        }

        private void gotoPageAdminTour_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminTour());
        }

        private void gotoPageSale_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminSale());
        }

        private void gotoPageUserMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUserMenu());
        }
    }
}
