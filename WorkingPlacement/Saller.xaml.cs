using FlowerStore.SallerWorkSpace;
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

namespace FlowerStore.WorkingPlacement
{
    /// <summary>
    /// Логика взаимодействия для Saller.xaml
    /// </summary>
    public partial class Saller : Window
    {
        public Saller()
        {
            InitializeComponent();
        }

        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = new Order2();
            order.Show();
            this.Close();
        }

        private void OpenClienBase_Click(object sender, RoutedEventArgs e)
        {
            var clientBase = new ClienBase();
            clientBase.Show();
            this.Close();
        }

        private void CloseBack_Click(object sender, RoutedEventArgs e)
        {
            //var authorization = new Autorization();
            //authorization.Show();
            this.Close();
        }
    }
}
