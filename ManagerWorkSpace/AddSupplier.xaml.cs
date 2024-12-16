using FlowerStore.SallerWorkSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace FlowerStore.ManagerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Page
    {


    private Supplier _currentSupplier = new Supplier();
    public AddSupplier(Supplier selectedSupplier)
    {
        InitializeComponent();
            _currentSupplier = selectedSupplier ?? new Supplier();
            DataContext = _currentSupplier;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Supplier1());
            StringBuilder errors = new StringBuilder();

            // Проверка на пустые значения
            if (string.IsNullOrEmpty(_currentSupplier.NameSupplier))
            {
                errors.AppendLine("Укажите имя");
            }

            if (string.IsNullOrEmpty(_currentSupplier.PhoneSupplier))
            {
                errors.AppendLine("Укажите Номер");
            }

            if (string.IsNullOrEmpty(_currentSupplier.AdressSupplier))
            {
                errors.AppendLine("Укажите Адрес");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление нового клиента
            if (_currentSupplier.IDSupplier == 0) // ID автоматически назначается
            {
                KursovoiEntities1.GetContext().Supplier.Add(_currentSupplier);
            }

            try
            {
                // Сохранение изменений в базе данных
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                return;

                // Навигация обратно на страницу клиентской базы
                // Используем NavigationService для перехода на ClientBasePage
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex onlyDigits = new Regex("[^0-9]+");
            e.Handled = onlyDigits.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text.Replace("-", ""); // Убираем дефисы для обработки
            string formattedText = "";

            // Если символов больше 11, показываем сообщение об ошибке
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
