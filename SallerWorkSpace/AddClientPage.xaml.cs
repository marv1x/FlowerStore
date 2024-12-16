using FlowerStore.WorkingPlacement;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FlowerStore.SallerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        private Client _currentClient;

        // Конструктор, принимающий объект клиента
        public AddClientPage(Client client)
        {
            InitializeComponent();
            _currentClient = client ?? new Client(); // Если передан null, создаём новый объект
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

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Проверяем, чтобы ввод был только цифрами
            Regex onlyDigits = new Regex("[^0-9]+");
            e.Handled = onlyDigits.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text.Replace("-", ""); // Убираем дефисы для обработки
            string formattedText = "";

            if (text.Length > 11)
            {
                
                textBox.Text = text.Substring(0, 11); // Ограничиваем ввод
                text = text.Substring(0, 11);
            }

            // Логика формирования маски
            for (int i = 0; i < text.Length; i++)
            {
                if (i == 1 || i == 4 || i == 7 || i == 9)
                    formattedText += "-";

                formattedText += text[i];
            }

            // Устанавливаем текст с маской
            textBox.Text = formattedText;
            textBox.CaretIndex = formattedText.Length; // Перемещаем курсор в конец
        }
    }
}
