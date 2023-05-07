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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DGridBook.ItemsSource = LibraryEntities.GetContext().Books.ToList();

            RegisterFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            Manager.mainWindow = this;

            Manager.RegisterFrame = RegisterFrame;
        }
    
        private void Button_Click_Autorization(object sender, RoutedEventArgs e)
        {
            if(TB_Login.Text == null || TB_Pass.Password == null)
            {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                var crypt = System.Security.Cryptography.SHA256.Create();
                var notfinal = crypt.ComputeHash(Encoding.UTF8.GetBytes(TB_Pass.Password));
                var final = Convert.ToBase64String(notfinal);

                Readers user = Manager.GetContext().Readers.FirstOrDefault(p => p.Reader_Login == TB_Login.Text && (p.Reader_Password.ToString() ==
                final));

                if (user != null)
                {
                    if(user.Admin == true)
                    {
                        AdminMenu windadmin = new AdminMenu();
                        windadmin.Show();
                        Close();
                    }
                    else { 
                    Manager.Login = user.Reader_Login;
                    UserMenu wind = new UserMenu();
                    wind.Show();
                    Close();
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Visibility = Visibility.Collapsed;
            RegisterFrame.Navigate(new PageRegistration());

        }

        public void VisibleMain()
        {
            RegisterFrame.Content = null;
            GridMain.Visibility = Visibility.Visible;
        }
    }
}
