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
        public Order2()
        {
            InitializeComponent();
            /// int productId = 1;
            /// Product product = KursovoiEntities.GetContext().Product.Where(p => p.IDProduct == productId).FirstOrDefault();
            /// MessageBox.Show("Product Name: " + product.NameProduct);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Services_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var Add = new AddClient();
            Add.Show();
            this.Close();
        }
    }
}
