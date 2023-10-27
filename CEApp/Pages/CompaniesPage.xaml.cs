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
    /// Логика взаимодействия для CompaniesPage.xaml
    /// </summary>
    public partial class CompaniesPage : Page
    {
        Repository _repository;
        public CompaniesPage(Repository repository)
        {
            InitializeComponent();
            _repository = repository;
        }

        private async void GetAllCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            InfoTextBox.Text = "";
            var response = await _repository.GetCompaniesAsync();
            foreach (var companies in response)
            {
                InfoTextBox.Text += "---------------------------------------------------" + "\n";
                InfoTextBox.Text += companies.Id.ToString() + "\n";
                InfoTextBox.Text += companies.Name + "\n";
                InfoTextBox.Text += companies.Address.ToString() + "\n";
                InfoTextBox.Text += companies.Country.ToString() + "\n";
            }
        }
    }
}
