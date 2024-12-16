using FlowerStore.SallerWorkSpace;
using FlowerStore.ManagerWorkSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using Xceed.Words.NET;
using Microsoft.Win32;
//using Xceed.Document.NET;
using Spire.Doc;
using Spire.Doc.Documents;

namespace FlowerStore.ManagerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Order3.xaml
    /// </summary>
    public partial class Order3 : Page
    {
        public Order3()
        {
            InitializeComponent();
            //DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Order.ToList();
            //DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
            //DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            //DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            LoadClientData();
        }

        private void LoadClientData()
        {
            DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Order.ToList();
            DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
            DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
        }

        private void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            var orderForRemoving = DGridOrder.SelectedItems.Cast<Order>().ToList();

            if (MessageBox.Show("Вы точно хотите удалить следующее", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursovoiEntities1.GetContext().Order.RemoveRange(orderForRemoving);
                    KursovoiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Order.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KursovoiEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LoadClientData();  // Перезагружаем данные клиентов
                DGridOrder.ItemsSource = KursovoiEntities1.GetContext().Order.ToList();
            }
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OrderPage(null));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выделенного работника
            var selectedOrder = DGridOrder.SelectedItem as Order;
            if (selectedOrder != null)
            {
                // Передаем объект работника на страницу редактирования
                this.NavigationService.Navigate(new OrderPage(selectedOrder));
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.");
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем выбранные даты
                DateTime? startDate = StartDatePicker.SelectedDate;
                DateTime? endDate = EndDatePicker.SelectedDate;

                // Получаем выбранный тип доставки
                string selectedDeliveryType = (DeliveryTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                // Загружаем заказы и применяем фильтр
                var filteredOrders = KursovoiEntities1.GetContext().Order.AsQueryable();

                if (startDate != null)
                {
                    filteredOrders = filteredOrders.Where(o => o.DateNTime >= startDate);
                }

                if (endDate != null)
                {
                    filteredOrders = filteredOrders.Where(o => o.DateNTime <= endDate);
                }

                if (!string.IsNullOrEmpty(selectedDeliveryType) && selectedDeliveryType != "Все")
                {
                    filteredOrders = filteredOrders.Where(o => o.DeliveryType == selectedDeliveryType);
                }

                // Обновляем таблицу
                DGridOrder.ItemsSource = filteredOrders.ToList();

                // Если данных нет, сообщаем пользователю
                if (!filteredOrders.Any())
                {
                    MessageBox.Show("Данные для выбранных фильтров отсутствуют.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно для выбора места сохранения
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx"; // Указываем тип файла
            saveFileDialog.DefaultExt = ".docx"; // Устанавливаем расширение по умолчанию

            // Проверяем, выбрал ли пользователь место для сохранения
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName; // Получаем путь сохранения

                // Теперь создаем и сохраняем документ в выбранную папку
                CreateAndSaveWordDocument(filePath);
            }
        }

        // Метод для создания и сохранения документа
        private void CreateAndSaveWordDocument(string filePath)
        {
            // Создаем новый документ Word
            Document document = new Document();

            // Добавляем раздел
            Section section = document.AddSection();

            // Добавляем заголовок документа
            Paragraph title = section.AddParagraph();
            title.AppendText("Отчет по заказам");
            title.ApplyStyle(BuiltinStyle.Title);
            title.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            // Добавляем строку для даты печати
            Paragraph datePrinted = section.AddParagraph();
            datePrinted.AppendText($"Дата печати: {DateTime.Now.ToString("dd.MM.yyyy")}");
            datePrinted.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

            // Если были выбраны даты, добавляем информацию о диапазоне дат
            if (StartDatePicker.SelectedDate.HasValue || EndDatePicker.SelectedDate.HasValue)
            {
                string startDateText = StartDatePicker.SelectedDate.HasValue
                    ? StartDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy")
                    : "не задана";
                string endDateText = EndDatePicker.SelectedDate.HasValue
                    ? EndDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy")
                    : "не задана";

                Paragraph dateRange = section.AddParagraph();
                dateRange.AppendText($"Диапазон дат: с {startDateText} по {endDateText}");
                dateRange.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;


            }

            string selectedDeliveryType = (DeliveryTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Не указан";

            string deliveryType = "Тип доставки: " + selectedDeliveryType;
            Paragraph deliveryParagraph = section.AddParagraph();
            deliveryParagraph.AppendText(deliveryType);
            deliveryParagraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;


            // Добавляем пустую строку для разделения
            section.AddParagraph();

            // Добавляем таблицу в документ
            Table ordersTable = section.AddTable(true);
            ordersTable.ResetCells(DGridOrder.Items.Count + 1, 7); // Увеличили до 8 столбцов
                                                                   // Включаем строку для заголовков

            // Заголовки столбцов
            ordersTable[0, 0].AddParagraph().AppendText("Код заказа");
            ordersTable[0, 1].AddParagraph().AppendText("Продукт");
            ordersTable[0, 2].AddParagraph().AppendText("Работник");
            ordersTable[0, 3].AddParagraph().AppendText("Клиент");
            ordersTable[0, 4].AddParagraph().AppendText("Количество");
            ordersTable[0, 5].AddParagraph().AppendText("Цена");
            ordersTable[0, 6].AddParagraph().AppendText("Дата");

            // Добавляем данные из DataGrid в таблицу
            for (int i = 0; i < DGridOrder.Items.Count; i++)
            {
                if (DGridOrder.Items[i] is Order order)
                {
                    ordersTable[i + 1, 0].AddParagraph().AppendText(order.IDOrder.ToString());
                    ordersTable[i + 1, 1].AddParagraph().AppendText(order.Product.NameProduct);
                    ordersTable[i + 1, 2].AddParagraph().AppendText(order.Worker1.FullName);
                    ordersTable[i + 1, 3].AddParagraph().AppendText(order.Client.FullName);
                    ordersTable[i + 1, 4].AddParagraph().AppendText(order.Status.ToString());
                    ordersTable[i + 1, 5].AddParagraph().AppendText(order.Cost.ToString());

                    if (order.DateNTime != null)
                    {
                        ordersTable[i + 1, 6].AddParagraph().AppendText(order.DateNTime.Value.ToString("dd.MM.yyyy"));
                    }
                    else
                    {
                        ordersTable[i + 1, 6].AddParagraph().AppendText("Дата отсутствует");
                    }
                }
            }

            // Сохраняем документ в выбранное место
            document.SaveToFile(filePath, FileFormat.Docx);
        }

        private void DeliveryTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
