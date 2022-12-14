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
    /// Логика взаимодействия для PageAdminSale.xaml
    /// </summary>
    public partial class PageAdminSale : Page
    {
        public PageAdminSale()
        {
            InitializeComponent();

            var dataSale = Base.EM.Sale.ToList();
            var dataClient = Base.EM.Client.ToList();
            var dataEmployee = Base.EM.Employee.ToList();
            var dataTour = Base.EM.Tour.ToList();



            var allData = from s in dataSale
                          join c in dataClient
                          on s.id_client equals c.id_client
                          join e in dataEmployee
                          on s.id_employee equals e.id_employee
                          join t in dataTour
                          on s.id_tour equals t.id_tour

                          select new { s.sale_date, c.name, c.surname, c.email, c.phone_number, eName = e.name, eSurname = e.surname, t.tour_name, t.price, t.departure_date, s.amount};

            listView.ItemsSource = allData.ToList();




            decimal ageSum = allData.Sum(p => p.price * p.amount);


            textBoxTotalPrice.Text = ("Выручка тек. месяца: " + string.Format("{0:F}", ageSum) + " руб." );
            }
        

        private void gotoPageAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }
    }
}
