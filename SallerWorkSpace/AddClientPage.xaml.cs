using FlowerStore.WorkingPlacement;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FlowerStore.SallerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        private Client _currentClient = new Client();
        public AddClientPage()
        {
            InitializeComponent();
            DataContext = _currentClient;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            // Проверка на пустые значения
            if (string.IsNullOrEmpty(_currentClient.FirstName))
            {
                errors.AppendLine("Укажите имя");
            }

            if (string.IsNullOrEmpty(_currentClient.LastName))
            {
                errors.AppendLine("Укажите Фамилию");
            }

            if (string.IsNullOrEmpty(_currentClient.MiddleName))
            {
                errors.AppendLine("Укажите Отчество");
            }

            if (string.IsNullOrEmpty(Convert.ToString(_currentClient.IDClient)))
            {
                errors.AppendLine("Укажите Айди");
            }

            if (string.IsNullOrEmpty(Convert.ToString(_currentClient.NumberClient)))
            {
                errors.AppendLine("Укажите Номер");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового клиента
            if (_currentClient.IDClient == 0) // ID автоматически назначается
            {
                KursovoiEntities1.GetContext().Client.Add(_currentClient);
            }

            try
            {
                // Сохранение изменений в базе данных
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");

                // Навигация обратно на страницу клиентской базы
                this.NavigationService.Navigate(new ClientBasePage()); // Используем NavigationService для перехода на ClientBasePage
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
