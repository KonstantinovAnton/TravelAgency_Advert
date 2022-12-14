using Microsoft.Win32;
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
using System.Drawing;
using System.IO;

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для WindowEditPhoto.xaml
    /// </summary>
    public partial class WindowEditPhoto : Window
    {

        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
            using (MemoryStream m = new MemoryStream(Barray))  // для считывания байтового потока
            {
                BI.BeginInit();  // начинаем считывание
                BI.StreamSource = m;  // задаем источник потока
                BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                BI.EndInit();  // заканчиваем считывание
            }
            img.Source = BI;  // показываем картинку на экране (imUser – имя картиник в разметке)
            img.Stretch = Stretch.Uniform;
        }

        PhotoUser[] arrayPhotoUser;
        int photoCount;
        int currentPhotoIndex;
     

        public WindowEditPhoto()
        {
            InitializeComponent();

            arrayPhotoUser = Base.EM.PhotoUser.Where(x => x.id_user == GlobalValues.id_user).ToArray();
            photoCount = arrayPhotoUser.Length;
            currentPhotoIndex = 0;
            

          

            if (arrayPhotoUser != null)
            {
                byte[] Bar = arrayPhotoUser[0].phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imageGallery);  // отображаем картинку
            }
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
            User user = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

            user.id_photo = arrayPhotoUser[currentPhotoIndex].id_photo;

            Base.EM.SaveChanges();

            MessageBox.Show("Фото успешно изменено", "Редактирование фото", MessageBoxButton.OK, MessageBoxImage.Information);



            MainWindow mainWindow = new MainWindow();
            mainWindow.frm.Navigate(new PageUserMenu());
            mainWindow.Show();

            this.Close();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            currentPhotoIndex--;
            if (currentPhotoIndex == -1)
            {
                currentPhotoIndex = photoCount-1;
            }

            if (arrayPhotoUser != null)
            {
                byte[] Bar = arrayPhotoUser[currentPhotoIndex].phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imageGallery);  // отображаем картинку
            }
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            currentPhotoIndex++;
            if (currentPhotoIndex == photoCount)
            {
                currentPhotoIndex = 0;
            }

            if (arrayPhotoUser != null)
            {
                byte[] Bar = arrayPhotoUser[currentPhotoIndex].phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imageGallery);  // отображаем картинку
            }

        }
    }
}
