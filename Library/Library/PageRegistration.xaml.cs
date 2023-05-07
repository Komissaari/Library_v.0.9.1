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
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        private Readers _currentReaders = new Readers();
        public PageRegistration()
        {
            InitializeComponent();
            DataContext = _currentReaders;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentReaders.Surname))
                errors.AppendLine("Пустое поле фамилии читателя!");
            if (string.IsNullOrWhiteSpace(_currentReaders.Name))
                errors.AppendLine("Пустое поле имени читателя!");
            if (string.IsNullOrWhiteSpace(TB_Pass.Password)) { 
                errors.AppendLine("Пустое поле пароля читателя!");
                
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                var crypt = System.Security.Cryptography.SHA256.Create();
                var notfinal = crypt.ComputeHash(Encoding.UTF8.GetBytes(TB_Pass.Password));
                var final = Convert.ToBase64String(notfinal);
                _currentReaders.Reader_Password = final;

                Manager.GetContext().Readers.Add(_currentReaders);
                Manager.GetContext().SaveChanges();
                MessageBox.Show("сохранено ...");
                Manager.mainWindow.VisibleMain();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainWindow.VisibleMain();
        }
    }
}
