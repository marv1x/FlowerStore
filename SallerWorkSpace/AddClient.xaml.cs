using FlowerStore.WorkingPlacement;
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

namespace FlowerStore.SallerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private Client _currentClient = new Client();
        public AddClient()
        {
            InitializeComponent();
            DataContext = _currentClient;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentClient.FirstName))
            {
                errors.AppendLine("Укажите имя");
            }

            if (string.IsNullOrEmpty(_currentClient.LastName)) { errors.AppendLine("Укажите Фамилию"); }
            if (string.IsNullOrEmpty(_currentClient.MiddleName)) { errors.AppendLine("Укажите Отчество"); }
            if (string.IsNullOrEmpty(_currentClient.MiddleName)) { errors.AppendLine("Укажите Отчество"); }
            if (string.IsNullOrEmpty(Convert.ToString(_currentClient.IDClient))) { errors.AppendLine("Укажите Айди"); }
            if (string.IsNullOrEmpty(Convert.ToString(_currentClient.NumberClient))) { errors.AppendLine("Укажите Номер"); }
            if (errors.Length > 0) {MessageBox.Show(errors.ToString()); return; }

            if (_currentClient.IDClient != 0)
            {
                KursovoiEntities1.GetContext().Client.Add(_currentClient);
            }

            try
            {
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                var client = new ClienBase();
                client.Show();
                this.Close();   
            }

            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
        }
    }
}
