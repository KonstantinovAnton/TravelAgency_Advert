using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для PageAdminTour.xaml
    /// </summary>
    public partial class PageAdminTour : Page
    {
      
        public PageAdminTour()
        {
            InitializeComponent();


            defComboBoxCountries();

            countAllRecords = 1;
            textBoxCountRecords.Text = "1";


           
            sortAndFilterData();

            numberCurrentPage = 0;
            

            lb1.FontWeight = FontWeights.Bold;
           

        }

        private void gotoPageAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAdminMenu());
        }

        private void gotoPageAdminAddTour_Click(object sender, RoutedEventArgs e)
        {
            Tour tour = null;
            NavigationService.Navigate(new PageAdminAddTour(tour, true));
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender; 
            // получаем доступ к Button из шаблона
            int index = Convert.ToInt32(btn.Uid);   // получаем числовой Uid элемента списка (в разметке предварительно нужно связать номер ячейки с номером кота в базе данных)

            // создаем объект, который содержит кота, информацию о котором нужно отредактировать
            Tour tour = Base.EM.Tour.FirstOrDefault(x => x.id_tour == index);

            // переход на страницу с редактированием (на ту же самую, где и добавляли кота)
            NavigationService.Navigate(new PageAdminAddTour(tour, false));
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;

            int index = Convert.ToInt32(btn.Uid);
            var tourForDelete = Base.EM.Tour.FirstOrDefault(x => x.id_tour == index);
            List<Sale> saleTourForDelete = Base.EM.Sale.Where(x => x.id_tour == index).ToList();

            if (MessageBox.Show("Удалить тур " + tourForDelete.tour_name + "?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (Sale t in saleTourForDelete)
                    {
                        Base.EM.Sale.Remove(t);
                    }


                    Base.EM.Tour.Remove(tourForDelete);
                    Base.EM.SaveChanges();

                    MessageBox.Show("Тур удален", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Тур не удален", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

            NavigationService.Navigate(new PageAdminTour());

        }

       

        private void defComboBoxCountries()
        {
            List<Country> data = new List<Country>();

            Country country1 = new Country();
            country1.id_country = 0;
            country1.country_name = "Все страны";

            data.Add(country1);

            List<Country> countries = Base.EM.Country.ToList();

            foreach (var item in countries)
            {
                data.Add(item);
            }

            comboBox.ItemsSource = data.ToList();

            comboBox.DisplayMemberPath = "country_name";
            comboBox.SelectedValuePath = "id_country";

            comboBox.SelectedIndex = 0;

            comboBoxSort.Items.Insert(0, "По названию");
            comboBoxSort.Items.Insert(1, "По цене");
            comboBoxSort.Items.Insert(2, "По кол-ву дней");

            comboBoxSort.SelectedIndex = 0;

            
        }

        int countRecodrsOnPage;
        int numberCurrentPage;
        int countAllRecords;
        int countPages;

        private void buttonFiltrAndSort_Click(object sender, RoutedEventArgs e)
        {
            numberCurrentPage = 0;
          
            try { Convert.ToInt32(textBoxCountRecords.Text); }
            catch { textBoxCountRecords.Text = "1"; }

            sortAndFilterData();


            Label[] lb = new Label[] { lb1, lb2, lb3, lb4, lb5 };

            int j = 1;

            foreach (var item in lb)
            {
                item.Content = "" + j;
                j++;
                item.Visibility = Visibility.Collapsed;
                item.FontWeight = FontWeights.Normal;
            }

            lb[0].FontWeight = FontWeights.Bold;

            if (countPages < 5)
            {
                for (int i = 0; i < countPages; i++)
                {
                    lb[i].Visibility = Visibility.Visible;
                }
            }
            else
            {
                foreach (var item in lb)
                {
                    item.Visibility = Visibility.Visible;
                }

            }

        }

      

        private void sortAndFilterData()
        {
            try
            {
                countRecodrsOnPage = Convert.ToInt32(textBoxCountRecords.Text);
            }
            catch
            {
                countRecodrsOnPage = 2;
            }

            var tour = Base.EM.Tour.ToList();
            var tour_type = Base.EM.Tour_Type.ToList();
            var city = Base.EM.City.ToList();
            var country = Base.EM.Country.ToList();
            var hotel = Base.EM.Hotel.ToList();
            var nut = Base.EM.Nutrition.ToList();

            var allData = from t in tour
                          join tt in tour_type
                          on t.id_tour_type equals tt.id_tour_type
                          join c in city
                          on t.departure_city_id equals c.id_city
                          join c2 in city
                          on t.return_city_id equals c2.id_city
                          join co in country
                          on t.id_country equals co.id_country
                          join h in hotel
                          on t.id_hotel equals h.id_hotel
                          join n in nut
                          on t.id_nutrition equals n.id_nutrition

                          select new
                          {
                              t.id_tour,
                              t.tour_name,
                              t.price,
                              t.departure_date,
                              t.return_date,
                              city1 = c.city_name,
                              city2 = c2.city_name,
                              t.days_amount,
                              co.country_name,
                              h.hotel_name,
                              n.nutrition_type,
                              t.tour_img
                          };


            // Фильтрация для мин. цены

            decimal minPrice;
            try
            {
                minPrice = Convert.ToDecimal(textBoxPriceMin.Text);
                allData = allData.Where(x => x.price >= minPrice);
            }
            catch { }

            // Фильтрация для макс. цены
            decimal maxPrice;
            try
            {
                maxPrice = Convert.ToDecimal(textBoxPriceMax.Text);
                allData = allData.Where(x => x.price <= maxPrice);
            }
            catch { }

            // Фильтрация для страны

            int idCountry = Convert.ToInt32(comboBox.SelectedValue.ToString());
            var countryName = Base.EM.Country.FirstOrDefault(x => x.id_country == idCountry);

            if (idCountry != 0)
            {
                allData = allData.Where(x => x.country_name == countryName.country_name);
            }

            // Фильтрация для названия тура

            if (textBoxTourName.Text != "" && textBoxTourName.Text != " ")
            {
                allData = allData.Where(x => x.tour_name.ToLower().Contains(textBoxTourName.Text.ToLower()));
            }

            // Фильтрация для питания

            if ((bool)checkBoxWithNutrition.IsChecked)
            {
                allData = allData.Where(x => x.nutrition_type == "Полное");
            }

            // Сортировка

            int sortBy = comboBoxSort.SelectedIndex;

            if ((bool)radioButtonDesc.IsChecked)
            {
                if (sortBy == 0)
                {
                    allData = allData.OrderByDescending(x => x.tour_name);
                }
                else if (sortBy == 1)
                {
                    allData = allData.OrderByDescending(x => x.price);
                }
                else if (sortBy == 2)
                {
                    allData = allData.OrderByDescending(x => x.days_amount);
                }
            }
            else
            {
                if (sortBy == 0)
                {
                    allData = allData.OrderBy(x => x.tour_name);
                }
                else if (sortBy == 1)
                {
                    allData = allData.OrderBy(x => x.price);
                }
                else if (sortBy == 2)
                {
                    allData = allData.OrderBy(x => x.days_amount);
                }
            }

            countAllRecords = allData.Count(); // Определяем общее кол-во туров по заданному фильтру и сортировке

       
            countPages = (int) Math.Ceiling((double)countAllRecords / countRecodrsOnPage);

            listView.ItemsSource = allData.Skip((numberCurrentPage - 1) * countRecodrsOnPage).Take(countRecodrsOnPage).ToList();

            if(allData.Count() == 0)
            {
                MessageBox.Show("По заданному фильтру не найдено ни одного тура(", "Туры не найдены", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void labelForward_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label[] lb = new Label[] { lb1, lb2, lb3, lb4, lb5 };

    
            numberCurrentPage = countPages;

            int numP = 0;

            foreach (var item in lb)
            {
                item.FontWeight = FontWeights.Normal;
                if (item.Visibility == Visibility.Visible)
                    numP++;
            }

            int j = countPages;

            for (int i = numP - 1; i >= 0; i--)
            { 
                lb[i].Content = "" + j;
                j--;
            }
            lb[numP-1].FontWeight = FontWeights.Bold;

            /*numberCurrentPage++;

            labelNumberCurrentPage.Content = "" + (numberCurrentPage + 1);

            */

            sortAndFilterData();

            
        }


        private void labelBack_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Label[] lb = new Label[] { lb1, lb2, lb3, lb4, lb5 };

            numberCurrentPage = 0;

            foreach (var item in lb)
            {
                item.FontWeight = FontWeights.Normal;
            }

            for (int i = 0; i < countPages && i < 5; i++)
            {
                lb[i].Content = "" + (i + 1);

            }
            lb[0].FontWeight = FontWeights.Bold;

            /* if (numberCurrentPage != 0)
             {
                 numberCurrentPage--;

                 labelNumberCurrentPage.Content = "" + (numberCurrentPage + 1);
             }*/


            sortAndFilterData();

        }

        private void textBoxCountRecords_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                countRecodrsOnPage = Convert.ToInt32(textBoxCountRecords.Text);

                if (countRecodrsOnPage <= 0)
                {
                   MessageBox.Show("Кол-во отображаемых туров на странице должно быть больше 0", "Ошибка отображения", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxCountRecords.Text = "2";

                }
                else
                   numberCurrentPage = 0;
             
                sortAndFilterData();

                Label[] lb = new Label[] { lb1, lb2, lb3, lb4, lb5 };

                int j = 1;

                foreach (var item in lb)
                {
                    item.Content = "" + j;
                    j++;
                    item.Visibility = Visibility.Collapsed;
                    item.FontWeight = FontWeights.Normal;
                }

                lb[0].FontWeight = FontWeights.Bold;

                if (countPages < 5)
                {
                    for (int i = 0; i < countPages; i++)
                    {
                        lb[i].Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    foreach (var item in lb)
                    {
                        item.Visibility = Visibility.Visible;
                    }

                }
            } catch{}
        }

        private void lb_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Label senderLabel = (Label)sender;

         
            int activePage = Convert.ToInt32(senderLabel.Content);

            numberCurrentPage = activePage;


            Label[] lb = new Label[] { lb1, lb2, lb3, lb4, lb5 };

            if (sender.Equals(lb4) && Convert.ToInt32(lb4.Content) != countPages - 1)
            {

                foreach (var item in lb)
                {
                    item.Content = " " + (1 + Convert.ToInt32(item.Content));
                }
            }
            if (sender.Equals(lb5) && Convert.ToInt32(lb5.Content) != countPages)
            {

                if (Convert.ToInt32(lb5.Content) != countPages - 1)
                {
                    foreach (var item in lb)
                    {
                        item.Content = " " + (2 + Convert.ToInt32(item.Content));
                    }
                }
                else
                {
                    foreach (var item in lb)
                    {
                        item.Content = " " + (1 + Convert.ToInt32(item.Content));
                    }
                }
            }
            if (sender.Equals(lb2) && Convert.ToInt32(lb2.Content) != 2)
            {

                foreach (var item in lb)
                {
                    item.Content = " " + (Convert.ToInt32(item.Content) - 1);
                }
            }
            if (sender.Equals(lb1) && Convert.ToInt32(lb1.Content) != 1)
            {

                if (Convert.ToInt32(lb1.Content) != 2)
                {
                    foreach (var item in lb)
                    {
                        item.Content = " " + ( Convert.ToInt32(item.Content) - 2);
                    }
                }
                else
                {
                    foreach (var item in lb)
                    {
                        item.Content = " " + (Convert.ToInt32(item.Content) - 1);
                    }
                }
            }

        
             foreach (var item in lb)
              {
                  if (Convert.ToInt32(item.Content) == activePage)
                      item.FontWeight = FontWeights.Bold;
                  else
                      item.FontWeight = FontWeights.Normal;
              }


            sortAndFilterData();
        }

      
    }
}
