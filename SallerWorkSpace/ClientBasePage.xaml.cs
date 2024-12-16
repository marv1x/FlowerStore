using FlowerStore.WorkingPlacement;
using MaterialDesignColors;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FlowerStore.SallerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для ClientBasePage.xaml
    /// </summary>
    public partial class ClientBasePage : Page
    {
        private Client _currentClient = new Client();
        public ClientBasePage()
        {
            InitializeComponent();
            _currentClient = new Client(); // Создаём новый объект, если он не передан
            DataContext = _currentClient;

            // Инициализация списков продуктов, работников и клиентов
            ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        }


        public ClientBasePage(Client selectedClient)
        {
            InitializeComponent();

            _currentClient = selectedClient ?? new Client(); // Если передан null, создаём новый объект
            DataContext = _currentClient;

            // Инициализация списков продуктов, работников и клиентов
            ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();

        }

        // Метод для загрузки данных о клиентах
        //private void LoadClientData(Client Se)
        //{
        //    ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        //}

        // Обработчик кнопки "Удалить"
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var clientForRemoving = ClientBaseInfo.SelectedItems.Cast<Client>().ToList();

            if (MessageBox.Show("Вы точно хотите удалить следующее", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursovoiEntities1.GetContext().Client.RemoveRange(clientForRemoving);
                    KursovoiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        // Обработчик кнопки "Добавить"
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Навигация на страницу добавления клиента
            this.NavigationService.Navigate(new AddClientPage(null));
        }

        // Обработчик кнопки "Выйти"
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Навигация обратно на страницу продавца
            this.NavigationService.Content = null;
        }

        // Обработчик события, когда окно становится видимым
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KursovoiEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();  // Перезагружаем данные клиентов
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выделенного работника
            var selectedClient = ClientBaseInfo.SelectedItem as Client;
            if (selectedClient != null)
            {
                // Передаем объект работника на страницу редактирования
                this.NavigationService.Navigate(new AddClientPage(selectedClient));
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.");
            }
        }
    }
}
