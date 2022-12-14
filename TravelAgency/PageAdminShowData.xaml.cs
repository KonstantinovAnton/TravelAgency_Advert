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
    /// Логика взаимодействия для PageAdminShowData.xaml
    /// </summary>
    public partial class PageAdminShowData : Page
    {
        public PageAdminShowData()
        {
            InitializeComponent();
            
            

            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();

            var allData = from u in users
                            join g in genders
                            on u.id_gender equals g.id_gender
                            select new {u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday};
            dataGridAdminShowAll.ItemsSource = allData.ToList();


        }

        private void buttonSortAskSurname_Click(object sender, RoutedEventArgs e)
        {
            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();
            var orUsers = from p in users
                                orderby p.name
                                select p;

            var allData = from u in orUsers
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();

        }

        private void buttonSortDescSurname_Click(object sender, RoutedEventArgs e)
        {
            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();
            var orUsers = from p in users
                          orderby p.name descending
                          select p;

            var allData = from u in orUsers
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();
        }

        private void buttonSortMale_Click(object sender, RoutedEventArgs e)
        {
            var users = Base.EM.User.ToList();
             var genders = Base.EM.Gender.ToList();
            var orUsers = users.Where(x => x.id_gender == 1);

            var allData = from u in orUsers
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();

       
        }

        private void buttonSortFemale_Click(object sender, RoutedEventArgs e)
        {
            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();
            var orUsers = users.Where(x => x.id_gender == 2);


            var allData = from u in orUsers
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();
        }

        private void buttonSearchName_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxSearch.Text;
            if(name == "")
            {
                MessageBox.Show("Введите имя пользователя", "Сортировка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();
            var orUsers = users.Where(x => x.name.ToLower().Contains(name.ToLower()));

            var allData = from u in orUsers
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();
        }

        private void buttonSearchLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxSearch.Text;
            if(login == "")
            {
                MessageBox.Show("Введите логин пользователя", "Сортировка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();
            var orUsers = users.Where(x => x.login.ToLower().Contains(login.ToLower()));

            var allData = from u in orUsers
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();
        }

        private void buttonShowFirstData_Click(object sender, RoutedEventArgs e)
        {
            var users = Base.EM.User.ToList();
            var genders = Base.EM.Gender.ToList();

            var allData = from u in users
                          join g in genders
                          on u.id_gender equals g.id_gender
                          select new { u.surname, u.name, u.patronymic, u.login, g.gender1, u.birthday };
            dataGridAdminShowAll.ItemsSource = allData.ToList();
        }

        private void gotoPageAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }
    }

}
