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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowerStore.ManagerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Worker.xaml
    /// </summary>
    public partial class Worker : Page
    {
        public Worker()
        {
            InitializeComponent();
            //DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();
            LoadClientData();


        }
        private void LoadClientData()
        {
            DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var clientForRemoving1 = DGridSupplier.SelectedItems.Cast<Worker1>().ToList();

            if (MessageBox.Show("Вы точно хотите удалить следующее", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursovoiEntities1.GetContext().Worker1.RemoveRange(clientForRemoving1);
                    KursovoiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
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
                DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Worker1.ToList();
            }
        }

        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddWorker(null));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выделенного работника
            var selectedWorker = DGridSupplier.SelectedItem as Worker1;
            if (selectedWorker != null)
            {
                // Передаем объект работника на страницу редактирования
                this.NavigationService.Navigate(new AddWorker(selectedWorker));
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.");
            }
        }
    }
    
}
