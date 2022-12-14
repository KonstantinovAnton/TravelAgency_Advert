using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для PageUserMenu.xaml
    /// </summary>
    public partial class PageUserMenu : Page
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

        public PageUserMenu()
        {

            
            InitializeComponent();

           

            if (GlobalValues.role == 1)
                buttonGotoMenu.Visibility = Visibility.Visible;

            var userObj = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

            var photo = Base.EM.PhotoUser.FirstOrDefault(x => x.id_photo == userObj.id_photo);

            if (photo != null)
            {
                byte[] Bar = photo.phot;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showImage(Bar, imagePhotoUser);  // отображаем картинку
            }




            textBlockName.Text = userObj.name;
            textBlockSurname.Text = userObj.surname;
            textBlockLogin.Text = userObj.login;
            textBlockBirthday.Text = userObj.birthday.ToString("dd.MM.yyyy");






        }



        private void buttonEditData_Click(object sender, RoutedEventArgs e)
        {

            WindowEditPersonalData windowEditPersonalData = new WindowEditPersonalData();

            windowEditPersonalData.Show();

            foreach (Window w in App.Current.Windows)
            {
                if(w != windowEditPersonalData)
                w.Close();
            }
           
        }

        private void buttonEditLoginAndPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowEditLoginAndPassword windowEditLoginAndPassword = new WindowEditLoginAndPassword();

            windowEditLoginAndPassword.Show();
            foreach (Window w in App.Current.Windows)
            {
                if (w != windowEditLoginAndPassword)
                    w.Close();
            }
        }

        

        private void buttonGotoMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }

        private void buttonEditPhoto_Click(object sender, RoutedEventArgs e)
        {


           PhotoUser[] arrayPhotoUser = Base.EM.PhotoUser.Where(x => x.id_user == GlobalValues.id_user).ToArray();

            if (arrayPhotoUser.Length == 0)
            {
                MessageBox.Show("У вас нет фотографий. Добавьте фото, чтобы можно было из чего-то выбирать", "Изменение фото", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (arrayPhotoUser.Length == 1)
            {
                MessageBox.Show("У вас всего одно фото. Добавьте ещё, чтобы можно было выбирать", "Изменение фото", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                WindowEditPhoto windowEditPhoto = new WindowEditPhoto();

                windowEditPhoto.Show();

                foreach (Window w in App.Current.Windows)
                {
                    if (w != windowEditPhoto)
                        w.Close();
                }
            }

        }

        private void buttonAddNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            PhotoUser photoUser = new PhotoUser();  // создание объекта для добавления записи в таблицу, где хранится фото
            photoUser.id_user = GlobalValues.id_user;  // присваиваем значение полю idUser (id авторизованного пользователя)

            OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                                                        //OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  // выбор папки для открытия
            OFD.ShowDialog();  // открываем диалоговое окно  

            try
            {
                string path = OFD.FileName;  // считываем путь выбранного изображения

                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);  // создаем объект для загрузки изображения в базу

                ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                photoUser.phot = Barray;  // заполяем поле photoBinary полученным байтовым массивом

                Base.EM.PhotoUser.Add(photoUser);

                Base.EM.SaveChanges();

                User user = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);


                user.id_photo = photoUser.id_photo;

                Base.EM.SaveChanges();


                MessageBox.Show("Фото успешно добавлено", "Добавление фото", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = new MainWindow();
                mainWindow.frm.Navigate(new PageUserMenu());
                mainWindow.Show();
                foreach (Window w in App.Current.Windows)
                {
                    if (w != mainWindow)
                        w.Close();
                }

            }
            catch
            {
                string path = OFD.FileName;  // извлекаем полный путь к картинке
                string[] arrayPath = path.Split('\\');  // разделяем путь к картинке в массив
                path = "\\" + arrayPath[arrayPath.Length - 3] + "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];  // записываем в бд путь, начиная с имени папки

                photoUser.source = path;
                Base.EM.PhotoUser.Add(photoUser);

                Base.EM.SaveChanges();

               

                User user = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);

                user.id_photo = photoUser.id_photo;

                Base.EM.SaveChanges();

                MessageBox.Show("Фото успешно добавлено", "Добавление фото", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void buttonAddSeveralPhoto_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                User user = Base.EM.User.FirstOrDefault(x => x.id_user == GlobalValues.id_user);


                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                OFD.Multiselect = true;  // открытие диалогового окна с возможностью выбора нескольких элементов
                if (OFD.ShowDialog() == true)  // пока диалоговое окно открыто, будет в цикле записывать каждое выбранное изображение в БД
                {
                    foreach (string file in OFD.FileNames)  // цикл организован по именам выбранных файлов
                    {
                        PhotoUser photoUser = new PhotoUser();  // создание объекта для добавления записи в таблицу, где хранится фото
                        photoUser.id_user = GlobalValues.id_user; // присваиваем значение полю idUser (id авторизованного пользователя)
                        string path = file;  // считываем путь выбранного изображения
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);  // создаем объект для загрузки изображения в базу
                        ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                        photoUser.phot = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                        Base.EM.PhotoUser.Add(photoUser);  // добавляем объект в таблицу БД
                        Base.EM.SaveChanges();
                        user.id_photo = photoUser.id_photo;
                    }
                    Base.EM.SaveChanges();

                    MessageBox.Show("Фото успешно добавлены", "Добавление фото", MessageBoxButton.OK, MessageBoxImage.Information);

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.frm.Navigate(new PageUserMenu());
                    mainWindow.Show();
                    foreach (Window w in App.Current.Windows)
                    {
                        if (w != mainWindow)
                            w.Close();
                    }
                }
            }
            catch
            {

            }
        }
    }
}
