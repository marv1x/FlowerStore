using Spire.Doc.Documents;
using Spire.Doc;
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

namespace FlowerStore.ManagerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            //DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();
            LoadClientData();
        }

        private void LoadClientData()
        {
            DGridProduct.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddProduct(null));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = DGridProduct.SelectedItem as Product;
            if (selectedProduct != null)
            {
                // Передаем объект на страницу добавления
                this.NavigationService.Navigate(new AddProduct(selectedProduct));
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var productForRemoving1 = DGridProduct.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show("Вы точно хотите удалить следующее", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursovoiEntities1.GetContext().Product.RemoveRange(productForRemoving1);
                    KursovoiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridProduct.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
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
                DGridProduct.ItemsSource = KursovoiEntities1.GetContext().Product.ToList();
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, есть ли данные в DataGrid
            if (DGridProduct.Items.Count == 0)
            {
                MessageBox.Show("Нет данных для печати.");
                return;
            }

            // Создаем новый документ Word
            Document document = new Document();

            // Добавляем раздел
            Section section = document.AddSection();

            // Добавляем заголовок документа
            Paragraph title = section.AddParagraph();
            title.AppendText("Отчет по продуктам");
            title.ApplyStyle(BuiltinStyle.Title);
            title.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;

            // Добавляем строку для даты печати
            Paragraph datePrinted = section.AddParagraph();
            datePrinted.AppendText($"Дата печати: {DateTime.Now.ToString("dd.MM.yyyy")}");
            datePrinted.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;

            // Добавляем пустую строку для разделения
            section.AddParagraph();

            // Добавляем таблицу в документ
            Table productsTable = section.AddTable(true);
            productsTable.ResetCells(DGridProduct.Items.Count + 1, 6); // Включаем строку для заголовков

            // Заголовки столбцов
            productsTable[0, 0].AddParagraph().AppendText("Код продукта");
            productsTable[0, 1].AddParagraph().AppendText("Название продукта");
            productsTable[0, 2].AddParagraph().AppendText("Стоимость");
            productsTable[0, 3].AddParagraph().AppendText("Количество");
            productsTable[0, 4].AddParagraph().AppendText("Описание");
            productsTable[0, 5].AddParagraph().AppendText("Поставщик");

            // Добавляем данные из DataGrid в таблицу
            for (int i = 0; i < DGridProduct.Items.Count; i++)
            {
                if (DGridProduct.Items[i] is Product product)
                {
                    productsTable[i + 1, 0].AddParagraph().AppendText(product.IDProduct.ToString());
                    productsTable[i + 1, 1].AddParagraph().AppendText(product.NameProduct);
                    productsTable[i + 1, 2].AddParagraph().AppendText(string.Format("{0:F2}", product.Price));
                    productsTable[i + 1, 3].AddParagraph().AppendText(product.AmountProduct.ToString());
                    productsTable[i + 1, 4].AddParagraph().AppendText(product.DescriptionProduct);
                    productsTable[i + 1, 5].AddParagraph().AppendText(product.Supplier?.NameSupplier ?? "Не указан");
                }
            }

            // Открыть диалог для сохранения файла
            var saveDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Word Document (*.docx)|*.docx",
                FileName = "Отчет_по_продуктам"
            };

            if (saveDialog.ShowDialog() == true)
            {
                string filePath = saveDialog.FileName;
                // Сохраняем документ в выбранное место
                document.SaveToFile(filePath, FileFormat.Docx);
                MessageBox.Show("Отчет сохранен.");
            }
        }

    }
}
