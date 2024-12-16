using FlowerStore.SallerWorkSpace;
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
using System.Windows.Threading;

namespace FlowerStore.WorkingPlacement
{
    /// <summary>
    /// Логика взаимодействия для Saller.xaml
    /// </summary>
    public partial class Saller : Window
    {
        private DispatcherTimer timer;
        public Saller()
        {
            InitializeComponent();

            // Создаём таймер
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Интервал обновления - 1 секунда
            timer.Tick += UpdateTime;                // Привязываем обработчик
            timer.Start();                           // Запускаем таймер

            // Устанавливаем начальное значение времени
            CurrentTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
            MainFrame.Navigated += MainFrame_Navigated;
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            // Обновляем текст с текущим временем
            CurrentTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderPage(null));
            FirstTextBox.Visibility = Visibility.Hidden;
            SecondTextBox.Text = "Оформление заказа";
            SecondTextBox.Visibility = Visibility.Visible;
            Back.IsEnabled = true;
        }

        private void OpenClienBase_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientBasePage());
            FirstTextBox.Visibility = Visibility.Hidden;
            SecondTextBox.Text = "Клиентская база";
            SecondTextBox.Visibility = Visibility.Visible;
            Back.IsEnabled = true;
        }

        private void CloseBack_Click(object sender, RoutedEventArgs e)
        {
            //var authorization = new Autorization();
            //authorization.Show();
            if (MessageBox.Show("Вы точно хотите выйти? Это может повлечь не сохранение данных", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите вернуться? Это может повлечь не сохранение данных", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Back.IsEnabled = false;
                MainFrame.Content = null;
                SecondTextBox.Text = string.Empty; // Очищаем текст
                FirstTextBox.Visibility = Visibility.Visible; // Восстанавливаем видимость, если нужно
                SecondTextBox.Visibility = Visibility.Hidden; // Скрываем текст
            }
                
        }
        private void ClienBase_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddClientPage(null));
            FirstTextBox.Visibility = Visibility.Hidden;
            SecondTextBox.Text = "Добавление клиента";
            SecondTextBox.Visibility = Visibility.Visible;
            Back.IsEnabled = true;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Если в Frame больше нет содержимого, очищаем текст
            if (MainFrame.Content == null)
            {
                SecondTextBox.Text = string.Empty; // Очищаем текст
                FirstTextBox.Visibility = Visibility.Visible; // Восстанавливаем видимость, если нужно
                SecondTextBox.Visibility = Visibility.Hidden; // Скрываем текст
            }
        }
    }
}
