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
        public ClientBasePage()
        {
            InitializeComponent();
            LoadClientData();
        }

        // Метод для загрузки данных о клиентах
        private void LoadClientData()
        {
            ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        }

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
                    LoadClientData();  // Обновляем данные
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
            this.NavigationService.Navigate(new AddClientPage());
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
                LoadClientData();  // Перезагружаем данные клиентов
            }
        }
    }
}
