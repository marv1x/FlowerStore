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
using System.Data.Entity;


namespace FlowerStore.SallerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Order2.xaml
    /// </summary>
    public partial class Order2 : Window
    {
        private Worker1 _currentWorker = new Worker1();
        private Client _currentClient = new Client();
        private Product _currentProduct = new Product();
        private Order _currentOrder = new Order();
        public Order2()
        {
            InitializeComponent();
            DataContext = _currentWorker;
            DataContext = _currentClient;
            DataContext = _currentProduct;
            ProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            WorkerInsertName.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            ClientInsertName.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
            /// int productId = 1;
            /// Product product = KursovoiEntities.GetContext().Product.Where(p => p.IDProduct == productId).FirstOrDefault();
            /// MessageBox.Show("Product Name: " + product.NameProduct);
        }



        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var Add = new AddClient();
            Add.Show();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentClient.FirstName))
            {
                errors.AppendLine("Укажите имя");
            }

            if (string.IsNullOrEmpty(_currentWorker.LastName)) { errors.AppendLine("Укажите Фамилию"); }
            if (string.IsNullOrEmpty(_currentClient.MiddleName)) { errors.AppendLine("Укажите Отчество"); }
            // ПОМЕНЯЙ НАЗВАНИЯ
            if (string.IsNullOrEmpty(_currentProduct.MiddleName)) { errors.AppendLine("Укажите Отчество"); }
            if (string.IsNullOrEmpty(Convert.ToString(_currentClient.IDClient))) { errors.AppendLine("Укажите Айди"); }
            if (string.IsNullOrEmpty(Convert.ToString(_currentClient.NumberClient))) { errors.AppendLine("Укажите Номер"); }
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }

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
