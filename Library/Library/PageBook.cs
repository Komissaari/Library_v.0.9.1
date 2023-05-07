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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class PageBook : Page
    {
        public PageBook()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridBook.ItemsSource = Manager.GetContext().Books.ToList();
            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
           
            Manager.MainFrame.Navigate(new PageBookEdit((sender as Button).DataContext as Books, true));
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageBookEdit(null, false));
        }

        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            var delBooks = DGridBook.SelectedItems.Cast<Books>().ToList();
            if(MessageBox.Show($"Вы точно хотите удалить {delBooks.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) 
                == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().Books.RemoveRange(delBooks);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridBook.ItemsSource = Manager.GetContext().Books.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

       
    }
}
