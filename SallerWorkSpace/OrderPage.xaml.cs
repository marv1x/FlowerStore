using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Windows.Controls.Primitives;
using FlowerStore.ManagerWorkSpace;
using Spire.Doc.Documents;
using Spire.Doc;

namespace FlowerStore.SallerWorkSpace
{
    public partial class OrderPage : Page
    {

        public ComboBox PublicProductInsertName { get; set; }
        public ComboBox PublicWorkerInsertName { get; set; }
        public ComboBox PublicClientInsertName { get; set; }
        public TextBox PublicStatusBox { get; set; }
        public TextBox PublicCostBox { get; set; }
        public CheckBox PublicDeliveryCheckBox { get; set; }

        public Order _currentOrder = new Order();
        
        public OrderPage()
        {
            InitializeComponent();
            _currentOrder = new Order(); // Создаём новый объект, если он не передан
            DataContext = _currentOrder;

            PublicProductInsertName = ProductInsertName;  // Присваиваем референс из XAML
            PublicWorkerInsertName = WorkerInsertName;
            PublicClientInsertName = ClientInsertName;
            PublicStatusBox = StatusBox;
            PublicCostBox = CostBox;
            PublicDeliveryCheckBox = DeliveryCheckBox;

            PublicProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            PublicWorkerInsertName.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            PublicClientInsertName.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        }
        public OrderPage(Order selectedOrder)
        {
            InitializeComponent();

            _currentOrder = selectedOrder ?? new Order(); // Если передан null, создаём новый объект
            DataContext = _currentOrder;

            // Инициализация списков продуктов, работников и клиентов
            ProductInsertName.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            WorkerInsertName.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            ClientInsertName.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();

            // Установка выбранных элементов для выпадающих списков
            ProductInsertName.SelectedItem = KursovoiEntities1.GetContext().Product
                .FirstOrDefault(p => p.IDProduct == _currentOrder.IDProduct);

            WorkerInsertName.SelectedItem = KursovoiEntities1.GetContext().Worker1
                .FirstOrDefault(w => w.IDWorker == _currentOrder.IDWorker);

            ClientInsertName.SelectedItem = KursovoiEntities1.GetContext().Client
                .FirstOrDefault(c => c.IDClient == _currentOrder.IDClient);
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите закрыть форму оформления заказа и открыть форму клиента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // Переход к странице добавления клиента
                this.NavigationService.Navigate(new AddClientPage(null));
            }
        }

        // Поле для хранения ошибок
        private StringBuilder errors = new StringBuilder();

        public StringBuilder Errors
        {
            get { return errors; }
        }

        public void Save_Click(object sender, RoutedEventArgs e)
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

            if (!string.IsNullOrWhiteSpace(StatusBox.Text))
            {
                if (int.TryParse(StatusBox.Text, out int status))
                {
                    if (status < 0)
                        errors.AppendLine("Укажите положительное число");
                }
            }

            if (string.IsNullOrWhiteSpace(CostBox.Text))
            {
                errors.AppendLine("Укажите Стоимость");
            }

            if (!string.IsNullOrWhiteSpace(CostBox.Text))
            {
                if (int.TryParse(CostBox.Text, out int cost))
                {
                    if (cost < 0)
                        errors.AppendLine("Укажите положительное число");
                }
            }

            // Выводим ошибки перед завершением метода
            Console.WriteLine("Errors inside Save_Click: " + errors.ToString());

            // Возвращаем ошибки в свойство, если нужно
            this.errors = errors; // Если errors - это поле класса, сохраняем результат

            //if (string.IsNullOrWhiteSpace(DateNTime.Text))
            //{
            //    errors.AppendLine("Укажите дату");
            //}

            //if (int.Parse(CostBox.Text) < 0 || int.Parse(StatusBox.Text) < 0)
            //{
            //    errors.AppendLine("Укажите положительное число");
            //}




            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var product = ProductInsertName.SelectedItem as Product;
            var worker = WorkerInsertName.SelectedItem as Worker1;
            var client = ClientInsertName.SelectedItem as Client;

            _currentOrder.IDProduct = product.IDProduct;
            _currentOrder.IDWorker = worker.IDWorker;
            _currentOrder.IDClient = client.IDClient;
            _currentOrder.Status = StatusBox.Text;
            _currentOrder.Cost = CostBox.Text;
            _currentOrder.DateNTime = DateTime.Now;
            _currentOrder.DeliveryType = DeliveryCheckBox.IsChecked == true ? "Доставка" : "Самовывоз";

            

            try
            {
                KursovoiEntities1.GetContext().Order.AddOrUpdate(_currentOrder);
                KursovoiEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");

                // Уведомление о печати чека
                if (MessageBox.Show("Хотите распечатать чек?", "Печать чека", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CreateCheckDocument(_currentOrder);
                }

                this.NavigationService.Navigate(new Order3());  // Переход на эту же страницу после сохранения
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void CreateCheckDocument(Order order)
        {
            try
            {
                // Открываем диалог выбора пути сохранения
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Word Documents (*.docx)|*.docx",
                    FileName = "Чек_заказа"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Создаем документ Word
                    Document document = new Document();

                    // Добавляем раздел
                    Section section = document.AddSection();

                    // Настоящий стиль чека
                    Paragraph title = section.AddParagraph();
                    title.AppendText("==== Кассовый чек ====");
                    title.ApplyStyle(BuiltinStyle.Title);
                    title.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;


                    section.AddParagraph();

                    // Добавляем информацию о заказе
                    section.AddParagraph().AppendText($"Дата: {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}");
                    section.AddParagraph().AppendText($"Номер заказа: {order.IDOrder}");
                    section.AddParagraph().AppendText($"------------------------------------");
                    section.AddParagraph().AppendText($"Продукт: {order.Product.NameProduct}");
                    section.AddParagraph().AppendText($"Количество: {order.Status}");
                    section.AddParagraph().AppendText($"Цена: {order.Cost} руб.");
                    section.AddParagraph().AppendText($"------------------------------------");
                    section.AddParagraph().AppendText($"ИТОГО: {order.Cost} руб.");
                    section.AddParagraph().AppendText($"------------------------------------");
                    section.AddParagraph().AppendText($"Продавец: {order.Worker1.FullName}");
                    section.AddParagraph().AppendText($"Клиент: {order.Client.FullName}");
                    section.AddParagraph().AppendText($"Спасибо за покупку!");
                    section.AddParagraph().AppendText("==========================");

                    // Сохраняем документ
                    document.SaveToFile(saveFileDialog.FileName, FileFormat.Docx);

                    MessageBox.Show($"Чек успешно сохранен: {saveFileDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании чека: {ex.Message}");
            }
        }

        private void CostBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateCost();
        }

        private void ProductInsertName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateCost();
        }

        private void CalculateCost()
        {
            // Проверяем, выбран ли продукт
            if (ProductInsertName.SelectedItem is Product selectedProduct)
            {
                // Проверяем, корректно ли введено количество
                if (int.TryParse(StatusBox.Text, out int quantity) && quantity > 0)
                {
                    // Получаем цену продукта из базы данных, обрабатывая nullable
                    decimal productPrice = selectedProduct.Price ?? 0; // Если null, то устанавливается 0
                    decimal totalCost = productPrice * quantity;

                    // Выводим стоимость в CostBox
                    CostBox.Text = totalCost.ToString("0.00");
                }
                else
                {
                    // Если количество некорректное, очищаем CostBox
                    CostBox.Text = string.Empty;
                }
            }
            else
            {
                // Если продукт не выбран, очищаем CostBox
                CostBox.Text = string.Empty;
            }
        }


        private void StatusBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateCost();
        }
    }
}
