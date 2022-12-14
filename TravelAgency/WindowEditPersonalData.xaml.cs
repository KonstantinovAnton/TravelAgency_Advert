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

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для WindowEditPersonalData.xaml
    /// </summary>
    public partial class WindowEditPersonalData : Window
    {
        User userForChanges;

        public WindowEditPersonalData()
        {
            InitializeComponent();

            var person = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);
            
            userForChanges = person;

            textBoxName.Text = person.name;
            textBoxSurname.Text = person.surname;
            textBoxPatronymic.Text = person.patronymic;

            if (person.id_gender == 1)
            {
                radioBtnMale.IsChecked = true;
            }
            else
            {
                radioBtnFemale.IsChecked = true;
            }

            datePickBirthday.SelectedDate = person.birthday;

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
            try
            {

                if (textBoxName.Text == null || textBoxName.Text == "")
                {
                    MessageBox.Show("Введите имя пользователя", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (textBoxSurname.Text == null || textBoxSurname.Text == "")
                {
                    MessageBox.Show("Введите фамилию пользователя", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (datePickBirthday.SelectedDate == null)
                {
                    MessageBox.Show("Не выбрана дата рождения", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (MessageBox.Show("Вы точно хотите сохранить изменения? ", "Изменение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }

                userForChanges.name = textBoxName.Text;
                userForChanges.surname = textBoxSurname.Text;
                userForChanges.patronymic = textBoxPatronymic.Text;

                if ((bool)radioBtnMale.IsChecked)
                    userForChanges.id_gender = 1;
                else
                    userForChanges.id_gender = 2;

                userForChanges.birthday = datePickBirthday.SelectedDate.Value;


                Base.EM.SaveChanges();

           

                MessageBox.Show("Успешно изменено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);

              
                
                MainWindow mainWindow = new MainWindow();
                mainWindow.frm.Navigate(new PageUserMenu());
                mainWindow.Show();

                this.Close();

               
            }
            catch
            {
                MessageBox.Show("Возникли ошибки на сервере. Попробуйте позже", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
           


            this.Close();
        }
    }
}
