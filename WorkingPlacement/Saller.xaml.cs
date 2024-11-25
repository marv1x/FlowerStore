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
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            // Обновляем текст с текущим временем
            CurrentTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderPage());
            FirstTextBox.Visibility = Visibility.Hidden;
            SecondTextBox.Visibility = Visibility.Visible;
        }

        private void OpenClienBase_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientBasePage());
        }

        private void CloseBack_Click(object sender, RoutedEventArgs e)
        {
            //var authorization = new Autorization();
            //authorization.Show();
            this.Close();
        }
    }
}
