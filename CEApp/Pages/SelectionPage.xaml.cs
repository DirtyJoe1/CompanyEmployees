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

namespace CEApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Page
    {
        Repository _repository;
        public SelectionPage(Repository repository)
        {
            InitializeComponent();
            _repository = repository;
        }

        private void CompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompaniesPage(_repository));
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GradesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
