using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Page
    {
        private Worker1 _currentWorker;
        public AddWorker()
        {
            InitializeComponent();
            _currentWorker = new Worker1(); // Создаём новый объект, если он не передан
            DataContext = _currentWorker;

            // Инициализация списка должностей
            PostInsertName.ItemsSource = KursovoiEntities1.GetContext().Post.ToList();
        }

        public AddWorker(Worker1 worker)
        {
            InitializeComponent();

            _currentWorker = worker ?? new Worker1(); // Если передан null, создаём новый объект
            DataContext = _currentWorker;

            // Инициализация списка должностей
            PostInsertName.ItemsSource = KursovoiEntities1.GetContext().Post.ToList();
            PostInsertName.SelectedItem = KursovoiEntities1.GetContext().Post.FirstOrDefault(p => p.IDPost == _currentWorker.IDPost);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (PostInsertName.SelectedIndex == -1)
            {
                errors.AppendLine("Укажите должность");
            }

            if (string.IsNullOrWhiteSpace(WorkerName1.Text))
            {
                errors.AppendLine("Укажите Имя");
            }

            if (string.IsNullOrWhiteSpace(LastName1.Text))
            {
                errors.AppendLine("Укажите фамилия");
            }

            if (string.IsNullOrWhiteSpace(MiddleName1.Text))
            {
                errors.AppendLine("Укажите отчество");
            }

            if (string.IsNullOrWhiteSpace(Number.Text))
            {
                errors.AppendLine("Укажите номер");
            }

            if (string.IsNullOrWhiteSpace(Adress1.Text))
            {
                errors.AppendLine("Укажите адрес");
            }

            if (string.IsNullOrWhiteSpace(Login1.Text))
            {
                errors.AppendLine("Укажите логин");
            }

            if (string.IsNullOrWhiteSpace(Password1.Text))
            {
                errors.AppendLine("Укажите пароль");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var post = PostInsertName.SelectedItem as Post;

            var worker = new Worker1
            {
                Number = Number.Text,
                FirstName = WorkerName1.Text,
                LastName = LastName1.Text,
                MiddleName = MiddleName1.Text,
                Adress = Adress1.Text,
                Login = Login1.Text,
                Password = Password1.Text,
                IDPost = post.IDPost,
            };


            try
            {
                KursovoiEntities1.GetContext().Worker1.AddOrUpdate(_currentWorker);
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                return;
            }

            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, чтобы ввод был только цифрами
            Regex onlyDigits = new Regex("[^0-9]+");
            e.Handled = onlyDigits.IsMatch(e.Text);
        }

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
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
