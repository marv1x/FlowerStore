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
using System.Data.Entity.Migrations;


namespace FlowerStore.SallerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Order2.xaml
    /// </summary>
    public partial class Order2 : Window
    {
        public Order2()
        {
            InitializeComponent();
            ProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            WorkerInsertName.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            ClientInsertName.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
            /// int productId = 1;
            /// Product product = KursovoiEntities.GetContext().Product.Where(p => p.IDProduct == productId).FirstOrDefault();
            /// MessageBox.Show("Product Name: " + product.NameProduct);
        }


        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть форму оформления заказа и открыть форму клиента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var Add = new AddClient();
                Add.Show();
                this.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ProductInsertName.SelectedIndex == -1)
            {
                errors.AppendLine("Укажите продукт");
            }

            if (WorkerInsertName.SelectedIndex == -1)
            {
                errors.AppendLine("Укажите работника");
            }

            if (ClientInsertName.SelectedIndex == -1)
            {
                errors.AppendLine("Укажите клиента");
            }

            if (string.IsNullOrWhiteSpace(StatusBox.Text))
            {
                errors.AppendLine("Укажите количество");
            }

            if (string.IsNullOrWhiteSpace(CostBox.Text))
            {
                errors.AppendLine("Укажите стоимость");
            }

            if (string.IsNullOrWhiteSpace(IDOrderBox.Text))
            {
                errors.AppendLine("Укажите Айди");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var product = ProductInsertName.SelectedItem as Product;
            var worker = WorkerInsertName.SelectedItem as Worker1;
            var client = ClientInsertName.SelectedItem as Client;

            var order = new Order
            {
                IDOrder = int.Parse(IDOrderBox.Text),
                IDProduct = product.IDProduct,
                IDWorker = worker.IDWorker,
                IDClient = client.IDClient,
                Status = StatusBox.Text,
                Cost = CostBox.Text,
                DateNTime = DateTime.Parse(DateNTime.Text)
            };


            try
            {
                KursovoiEntities1.GetContext().Order.AddOrUpdate(order);
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                var Order2 = new Order2();
                Order2.Show();
                this.Close();
            }

            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
        }
    }
}
