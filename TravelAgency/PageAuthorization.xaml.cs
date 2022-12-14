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
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        public PageAuthorization()
        {
            InitializeComponent();
        }

        private void btnAuthorizate_Click(object sender, RoutedEventArgs e)
        {



            int pass = passBox.Password.ToString().GetHashCode();

            try
            {
                var userObj = Base.EM.User.FirstOrDefault(x => x.login == textBoxLogin.Text && x.password == pass);

                if (userObj == null)
                {
                    MessageBox.Show("Таких нет!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    GlobalValues.id_user = userObj.id_user;
                    GlobalValues.role = userObj.id_role;

                    switch (userObj.id_role)
                    {
                        case 1:

                            NavigationService.Navigate(new PageAdminMenu());

                            break;

                        case 2:

                            NavigationService.Navigate(new PageUserMenu());
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Возникли проблемы с соединением с сервером. Пожалуйста, повторите ещё раз","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
