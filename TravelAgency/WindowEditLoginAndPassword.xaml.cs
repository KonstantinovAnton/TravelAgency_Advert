using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для WindowEditLoginAndPassword.xaml
    /// </summary>
    public partial class WindowEditLoginAndPassword : Window
    {
        User userForChanges;
        public WindowEditLoginAndPassword()
        {
            InitializeComponent();

            var person = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

            userForChanges = person;

            textBoxLogin.Text = person.login;




        }

        private void buttonGotoPageUserMenu_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.frm.Navigate(new PageUserMenu());
            mainWindow.Show();

            this.Close();
        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == null || textBoxLogin.Text == "")
            {
                MessageBox.Show("Введите новый логин", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var userObj = Base.EM.User.FirstOrDefault(x => x.login == textBoxLogin.Text);

            if (userObj != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка изменения", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (passBox.Password != passBoxRepeatPass.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (passBox.Password != null && passBox.Password != "")
                if (!isCorrectPass(passBox))
                     return;

            if (MessageBox.Show("Вы точно хотите сохранить изменения? ", "Изменение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                if (passBox.Password != null && passBox.Password != "")
                    userForChanges.password = passBox.Password.ToString().GetHashCode();

                userForChanges.login = textBoxLogin.Text;
                

                Base.EM.SaveChanges();
                MessageBox.Show("Успешно изменено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);


                MainWindow mainWindow = new MainWindow();
                mainWindow.frm.Navigate(new PageUserMenu());
                mainWindow.Show();

                this.Close();
            }
            catch
            {
                MessageBox.Show("Возникли ошибки на сервере, попробуйте позже", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           

        }

        private bool isCorrectPass(PasswordBox pb)
        {
            string pass = pb.Password;

            Regex regBigLat = new Regex("(?=.*[A-Z])"); // Одна большая латинская
            Regex regSmallLat = new Regex("(?=.*[a-z])"); // Три строчных латинских
            Regex regDig = new Regex("(?=.*[0-9])"); // Две цифры
            Regex regSpec = new Regex("(?=.*[!@#$%^&*()+=])"); // Спец символы



            if (!(regBigLat.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержится ни один заглавный латинский символ", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);


                return false;
            }
            if (!(regSmallLat.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержится 3 прописных латинских символа", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            if (!(regDig.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержится 2 цифр", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            if (!(regSpec.IsMatch(pass)))
            {
                MessageBox.Show("В вашем пароле не содержутся спец. символы", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            if (pass.Length < 8)
            {
                MessageBox.Show("В вашем пароле менее 8 символов", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            else return true;
        }


    }
}
