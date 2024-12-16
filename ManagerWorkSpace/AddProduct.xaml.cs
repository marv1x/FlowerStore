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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        private Product _currentProduct = new Product();
        

        public AddProduct()
        {
            InitializeComponent();
            _currentProduct = new Product(); // Создаём новый объект, если он не передан
            DataContext = _currentProduct;

            // Инициализация списков продуктов, работников и клиентов
            ProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();
        }
        public AddProduct(Product selectedProduct)
        {
            InitializeComponent();

            _currentProduct = selectedProduct ?? new Product(); // Если передан null, создаём новый объект
            DataContext = _currentProduct;

            // Инициализация списков продуктов, работников и клиентов
            ProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();

            // Установка выбранных элементов для выпадающих списков
            ProductInsertName.SelectedItem = KursovoiEntities1.GetContext().Supplier
                .FirstOrDefault(p => p.IDSupplier == _currentProduct.IDSupplier);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            // Проверка на пустые значения
            if (string.IsNullOrEmpty(_currentProduct.NameProduct))
            {
                errors.AppendLine("Укажите продукт");
            }

            if (string.IsNullOrEmpty(Convert.ToString(_currentProduct.Price)))
            {
                errors.AppendLine("Укажите стоимость");
            }

            if (string.IsNullOrEmpty(Convert.ToString(_currentProduct.AmountProduct)))
            {
                errors.AppendLine("Укажите количество");
            }

            if (ProductInsertName.SelectedIndex == -1)
            {
                errors.AppendLine("Укажите поставщика");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var product = ProductInsertName.SelectedItem as Supplier;


            var product1 = new Product
            {

                NameProduct = NameProduct1.Text,
                Price = int.Parse(Price1.Text),
                AmountProduct = int.Parse(Amount1.Text),
                DescriptionProduct = Description1.Text,
            };




            try
            {
                // Сохранение изменений в базе данных
                KursovoiEntities1.GetContext().Product.AddOrUpdate(_currentProduct);
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                this.NavigationService.Navigate(new Products());

                // Навигация обратно на страницу клиентской базы
                // Используем NavigationService для перехода на ClientBasePage
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
