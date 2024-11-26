using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Windows.Controls.Primitives;

namespace FlowerStore.SallerWorkSpace
{
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            ProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            WorkerInsertName.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            ClientInsertName.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть форму оформления заказа и открыть форму клиента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // Переход к странице добавления клиента
                this.NavigationService.Navigate(new AddClientPage());
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

            if (string.IsNullOrWhiteSpace(DateNTime.Text))
            {
                errors.AppendLine("Укажите дату");
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
                this.NavigationService.Navigate(new OrderPage());  // Переход на эту же страницу после сохранения
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
