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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowerStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            KursovoiEntities1 model = new KursovoiEntities1();
            try
            {
                var userobj = model.Worker1.FirstOrDefault(x => x.Login == LoginTextBox.Text && x.Password == PasswordBox.Password);
                if (userobj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    AccountHelpClass.Id = userobj.IDWorker;
                    switch (userobj.IDPost)
                    {
                        case 1:
                            var manager = new Manager();
                            manager.Show();
                            this.Close();
                            break;
                        case 2:
                            var saller = new Saller();
                            saller.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Критическая ошибка работы приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
