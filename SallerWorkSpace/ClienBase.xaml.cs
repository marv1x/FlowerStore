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
    /// Логика взаимодействия для ClienBase.xaml
    /// </summary>
    public partial class ClienBase : Window
    {
        public ClienBase()
        {
            InitializeComponent();

            //ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var ClientForRemoving = ClientBaseInfo.SelectedItems.Cast<Client>().ToList();

            if (MessageBox.Show("Вы точно хотите удалить следующее", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursovoiEntities1.GetContext().Client.RemoveRange(ClientForRemoving);
                    KursovoiEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var Add = new AddClient();
            Add.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            /// РАЗОБРАТЬСЯ ВАТАХЕЛЬСИНКИ
            ///AppFrame.SallerFrame.Navigate(new WorkingPlacement.Saller());
            ///
            var saller = new Saller();
            saller.Show();
            this.Close();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible) 
            { 
           KursovoiEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ClientBaseInfo.ItemsSource = KursovoiEntities1.GetContext().Client.ToList();
            }
        }
    }
}
