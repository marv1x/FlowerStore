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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowerStore.ManagerWorkSpace
{
    /// <summary>
    /// Логика взаимодействия для Supplier1.xaml
    /// </summary>
    public partial class Supplier1 : Page
    {
        
        public Supplier1()
        {
            InitializeComponent();
            //DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();
            LoadClientData();
            

        }
        private void LoadClientData()
        {
            DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var clientForRemoving1 = DGridSupplier.SelectedItems.Cast<Supplier>().ToList();

            if (MessageBox.Show("Вы точно хотите удалить следующее", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursovoiEntities1.GetContext().Supplier.RemoveRange(clientForRemoving1);
                    KursovoiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();
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
                DGridSupplier.ItemsSource = KursovoiEntities1.GetContext().Supplier.ToList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddSupplier(null));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = DGridSupplier.SelectedItem as Supplier;
            if (selectedSupplier != null)
            {
                // Передаем объект на страницу добавления
                this.NavigationService.Navigate(new AddSupplier(selectedSupplier));
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.");
            }
        }

    }
}
