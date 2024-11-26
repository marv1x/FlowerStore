using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
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
        public AddWorker()
        {
            InitializeComponent();
            PostInsertName.ItemsSource = KursovoiEntities1.GetContext().Post.ToList();
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
                Number = int.Parse(Number.Text),
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
                KursovoiEntities1.GetContext().Worker1.AddOrUpdate(worker);
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                return;
            }

            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
        }
    }
}
