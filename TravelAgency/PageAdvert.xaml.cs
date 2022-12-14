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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgency
{
    /// <summary>
    /// Логика взаимодействия для PageAdvert.xaml
    /// </summary>
    public partial class PageAdvert : Page
    {
        public PageAdvert()
        {
            InitializeComponent();

            textBlock.Text = "Туристическая компания «КонТур» приветствует Вас!\nНаша компания рада предложить Вам огромный выбор \nразнообразных туров и экскурсий как по России, так и \n по различным странам мира." +

"\nЯвляясь туроператором по международному \nвъездному и  внутреннему туризму \nмы предлагаем автобусные туры с экскурсионной \nпрограммой по городам России; автобусные туры с отдыхом \nна черноморском побережье; прием организованных групп в\nНижегородской области; собираем сборные \nгруппы в Нижнем Новгороде на экскурсии и туры. " +

"\nМы обещаем никогда не останавливаться на достигнутых \nрезультатах, постоянно предлагая Вам новые услуги\n и программы, расширяя географию наших туров\n" +

"\nНаша миссия: Каждый человек нуждается в полноценном \nотдыхе, свободном от повседневной \nрутины и будничных забот." +

"\n\nНаши принципы: Для каждого из нас нет задачи важнее, \nчем удовлетворить \nВаши пожелания и оправдать Ваше доверие. Мы стремимся \nдостичь высокого мастерства в нашем деле, чтобы \nгарантировать Вам непревзойденный сервис. И поэтому мы \nнеустанно заботимся о качестве наших услуг, \nчтобы Вы могли наслаждаться отдыхом в кругу друзей и семьи.";
              
                
               

            DoubleAnimation lblName = new DoubleAnimation();
            lblName.From = 30; // начальное значение свойства
            lblName.To = 40; // конечное значение свойства
            lblName.Duration = TimeSpan.FromSeconds(2); // продолжительность анимации (в секундах)
            lblName.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            lblName.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            labelName.BeginAnimation(FontSizeProperty, lblName); // «навешивание» анимации на свойство ширины кнопки

            DoubleAnimation butWidth = new DoubleAnimation();
            butWidth.From = 100; // начальное значение свойства
            butWidth.To = 120; // конечное значение свойства
            butWidth.Duration = TimeSpan.FromSeconds(2); // продолжительность анимации (в секундах)
            butWidth.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            butWidth.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            buttonGotoRegistration.BeginAnimation(WidthProperty, butWidth); // «навешивание» анимации на свойство ширины кнопки

            DoubleAnimation butHeight = new DoubleAnimation();
            butHeight.From = 30; // начальное значение свойства
            butHeight.To = 40; // конечное значение свойства
            butHeight.Duration = TimeSpan.FromSeconds(2); // продолжительность анимации (в секундах)
            butHeight.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            butHeight.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            buttonGotoRegistration.BeginAnimation(HeightProperty, butHeight); // «навешивание» анимации на свойство ширины кнопки

            DoubleAnimation imgWidth = new DoubleAnimation();
            imgWidth.From = 100; // начальное значение свойства
            imgWidth.To = 120; // конечное значение свойства
            imgWidth.Duration = TimeSpan.FromSeconds(2); // продолжительность анимации (в секундах)
            imgWidth.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            imgWidth.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            logo.BeginAnimation(WidthProperty, imgWidth); // «навешивание» анимации на свойство ширины кнопки

            DoubleAnimation imgHeight = new DoubleAnimation();
            imgHeight.From = 100; // начальное значение свойства
            imgHeight.To = 120; // конечное значение свойства
            imgHeight.Duration = TimeSpan.FromSeconds(2); // продолжительность анимации (в секундах)
            imgHeight.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            imgHeight.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            logo.BeginAnimation(HeightProperty, imgHeight); // «навешивание» анимации на свойство ширины кнопки

            ThicknessAnimation MA = new ThicknessAnimation(); // анимация границ
            MA.From = new Thickness(10, 10, 10, 10);
            MA.To = new Thickness(0, 0, 0, 0);
            MA.Duration = TimeSpan.FromSeconds(5);
            MA.RepeatBehavior = RepeatBehavior.Forever;
            MA.AutoReverse = true;
            buttonGotoRegistration.BeginAnimation(MarginProperty, MA);

            ColorAnimation BA = new ColorAnimation(); // создание объекта для настройки анимации
            ColorConverter CC = new ColorConverter(); // создание объекта для конвертации кода в цвет
            Color Cstart = (Color)CC.ConvertFrom("#ff0000"); // присваивание начального цвета эл-ту
            buttonGotoRegistration.Background = new SolidColorBrush(Cstart); // закрашивание эл-та сплошным цветом
            BA.From = Cstart; // начальное значение свойства
            BA.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            BA.To = (Color)CC.ConvertFrom("#00ff00"); // конечное значение свойства
            BA.Duration = TimeSpan.FromSeconds(5);
            buttonGotoRegistration.Background.BeginAnimation(SolidColorBrush.ColorProperty, BA);
        }

        private void buttonGotoRegistration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageRegistration());
        }
    }
}
