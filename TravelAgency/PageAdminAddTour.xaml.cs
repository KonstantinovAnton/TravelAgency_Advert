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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgency
{
    

    /// <summary>
    /// Логика взаимодействия для PageAdminAddTour.xaml
    /// </summary>
    public partial class PageAdminAddTour : Page
    {
        Tour tourUpdated;
        string path;
        bool isNew;

        public PageAdminAddTour(Tour tour, bool isNew)
        {

            InitializeComponent();

            listBoxDepartCity.ItemsSource = Base.EM.City.ToList();
            listBoxDepartCity.SelectedValuePath = "id_city";
            listBoxDepartCity.DisplayMemberPath = "city_name";


            listBoxReturnCity.ItemsSource = Base.EM.City.ToList();
            listBoxReturnCity.SelectedValuePath = "id_city";
            listBoxReturnCity.DisplayMemberPath = "city_name";

            listBoxTourType.ItemsSource = Base.EM.Tour_Type.ToList();
            listBoxTourType.SelectedValuePath = "id_tour_type";
            listBoxTourType.DisplayMemberPath = "tour_type1";

            listBoxNutrition.ItemsSource = Base.EM.Nutrition.ToList();
            listBoxNutrition.SelectedValuePath = "id_nutrition";
            listBoxNutrition.DisplayMemberPath = "nutrition_type";

            listBoxHotel.ItemsSource = Base.EM.Hotel.ToList();
            listBoxHotel.SelectedValuePath = "id_hotel";
            listBoxHotel.DisplayMemberPath = "hotel_name";

            textBoxFindDepartCity.Text = "Введите город для поиска";
            textBoxFindReturnCity.Text = "Введите город для поиска";




            this.isNew = isNew;
            tourUpdated = tour;

            if (!isNew)
            {
               
                textBoxTourName.Text = tour.tour_name;
                textBoxPrice.Text = Convert.ToString(tour.price);
                listBoxDepartCity.SelectedValue = tour.departure_city_id;
                listBoxReturnCity.SelectedValue = tour.return_city_id;

                listBoxTourType.SelectedValue = tour.id_tour_type;
                listBoxNutrition.SelectedValue = tour.id_nutrition;
                listBoxHotel.SelectedValue = tour.id_hotel;

                

                dataPickerDepart.SelectedDate = tour.departure_date;
                dataPickerReturn.SelectedDate = tour.return_date;

                path = tour.tour_img;

                buttonAddTour.Content = "Изменить";
                
            }


        }

        private void listBoxReturnCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idCity = Convert.ToInt32(listBoxReturnCity.SelectedValue.ToString());
           
            var count = Base.EM.City.FirstOrDefault(x => x.id_city == idCity);
           
            var counrtyName = Base.EM.Country.FirstOrDefault(x => x.id_country == count.id_country);

            labelCountry.Content = counrtyName.country_name;


            var hotel = Base.EM.Hotel.Where(x => x.id_city == idCity);

            listBoxHotel.ItemsSource = hotel.ToList();
            listBoxHotel.SelectedValuePath = "id_hotel";
            listBoxHotel.DisplayMemberPath = "hotel_name";


        }

        private void dataPickerDepart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataPickerReturn.SelectedDate != null)
            {
                string days = Convert.ToString(dataPickerReturn.SelectedDate.Value - dataPickerDepart.SelectedDate.Value);
                string[] days1 = days.Split('.');
                labelDaysAmount.Content = days1[0];
            }

        }

        private void dataPickerReturn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataPickerDepart.SelectedDate != null)
            {
                string days = Convert.ToString(dataPickerReturn.SelectedDate.Value - dataPickerDepart.SelectedDate.Value);
                string[] days1 = days.Split('.');
                labelDaysAmount.Content = days1[0];
            }
        }

        private void buttonAddTour_Click(object sender, RoutedEventArgs e)
        {

            if (!checkValues()) return;

            int idCity = Convert.ToInt32(listBoxReturnCity.SelectedValue.ToString());
            var count = Base.EM.City.FirstOrDefault(x => x.id_city == idCity);
            string days = Convert.ToString(dataPickerReturn.SelectedDate.Value - dataPickerDepart.SelectedDate.Value);
            string[] days1 = days.Split('.');

            try
            {
                if (isNew)
                {
                    Tour tour = new Tour()
                    {
                        tour_name = textBoxTourName.Text,
                        price = Convert.ToDecimal(textBoxPrice.Text),
                        departure_date = dataPickerDepart.SelectedDate.Value,
                        departure_city_id = Convert.ToInt32(listBoxDepartCity.SelectedValue),
                        return_date = dataPickerReturn.SelectedDate.Value,
                        return_city_id = Convert.ToInt32(listBoxReturnCity.SelectedValue),
                        days_amount = Convert.ToInt32(days1[0]),
                        id_country = count.id_country,
                        id_tour_type = Convert.ToInt32(listBoxTourType.SelectedValue),
                        id_nutrition = Convert.ToInt32(listBoxNutrition.SelectedValue),
                        id_hotel = Convert.ToInt32(listBoxHotel.SelectedValue),
                        tour_img = path
                    };
                    Base.EM.Tour.Add(tour);
                    Base.EM.SaveChanges();

                    MessageBox.Show("Тур успешно добавлен", "Добавление тура", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    tourUpdated.tour_name = textBoxTourName.Text;
                    tourUpdated.price = Convert.ToDecimal(textBoxPrice.Text);
                    tourUpdated.departure_date = dataPickerDepart.SelectedDate.Value;
                    tourUpdated.departure_city_id = Convert.ToInt32(listBoxDepartCity.SelectedValue);
                    tourUpdated.return_date = dataPickerReturn.SelectedDate.Value;
                    tourUpdated.return_city_id = Convert.ToInt32(listBoxReturnCity.SelectedValue);
                    tourUpdated.days_amount = Convert.ToInt32(days1[0]);
                    tourUpdated.id_country = count.id_country;
                    tourUpdated.id_tour_type = Convert.ToInt32(listBoxTourType.SelectedValue);
                    tourUpdated.id_nutrition = Convert.ToInt32(listBoxNutrition.SelectedValue);
                    tourUpdated.id_hotel = Convert.ToInt32(listBoxHotel.SelectedValue);
                    tourUpdated.tour_img = path;

                    
                    Base.EM.SaveChanges();
                    MessageBox.Show("Тур успешно изменен", "Изменение тура", MessageBoxButton.OK, MessageBoxImage.Information);
                   
                }

                

                NavigationService.Navigate(new PageAdminTour());

            }
            catch {
                MessageBox.Show("Тур не добавлен", "Добавление тура", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAddChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();  // создаем объект диалогового окна
            OFD.ShowDialog();  // открываем диалоговое окно

            try
            {
                path = OFD.FileName;  // извлекаем полный путь к картинке
                string[] arrayPath = path.Split('\\');  // разделяем путь к картинке в массив
                path = "\\" + arrayPath[arrayPath.Length - 3] + "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];  // записываем в бд путь, начиная с имени папки
            }
            catch
            {
                return;
            }
            
        }

        private void gotoPageAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }

        private bool checkValues()
        {
            if (textBoxTourName.Text == " " || textBoxTourName.Text == "")
            {
                MessageBox.Show("Введите название тура", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                if (textBoxPrice.Text == "" || textBoxPrice.Text == " ")
                {
                    MessageBox.Show("Введите цену тура", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if(Convert.ToDecimal(textBoxPrice.Text) <= 0)
                {
                    MessageBox.Show("Введите корректную цену", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }
            catch
            {
                MessageBox.Show("Введите корректную цену", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(dataPickerDepart.SelectedDate== null)
            {
                MessageBox.Show("Введите дату отправки", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dataPickerReturn.SelectedDate == null)
            {
                MessageBox.Show("Введите дату возращения", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(dataPickerReturn.SelectedDate <= dataPickerDepart.SelectedDate)
            {
                MessageBox.Show("Дата возвращения должна быть после даты отправки", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(listBoxDepartCity.SelectedValue == null)
            {
                MessageBox.Show("Выберите город отправки из России", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listBoxDepartCity.SelectedValue == null)
            {
                MessageBox.Show("Выберите город возвращения из тура", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listBoxHotel.SelectedValue == null)
            {
                MessageBox.Show("Выберите отель", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listBoxNutrition.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип питания", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (listBoxTourType.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип тура", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void textBoxFindDepartCity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textBoxFindDepartCity.Text = null;
        }

        private void buttonFindDepartCity_Click(object sender, RoutedEventArgs e)
        {
            string cityDepart = textBoxFindDepartCity.Text;

            var query = Base.EM.City.Where(x => x.city_name.ToLower().Contains(cityDepart.ToLower()));

            listBoxDepartCity.ItemsSource = query.ToList();
            listBoxDepartCity.SelectedValuePath = "id_city";
            listBoxDepartCity.DisplayMemberPath = "city_name";

        }

        private void textBoxFindReturnCity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textBoxFindReturnCity.Text = null;
        }

        private void buttonFindReturnCity_Click(object sender, RoutedEventArgs e)
        {
            string cityReturn = textBoxFindReturnCity.Text;

            var query = Base.EM.City.Where(x => x.city_name.ToLower().Contains(cityReturn.ToLower()));

            listBoxReturnCity.ItemsSource = query.ToList();
            listBoxReturnCity.SelectedValuePath = "id_city";
            listBoxReturnCity.DisplayMemberPath = "city_name";
        }
    }

}
