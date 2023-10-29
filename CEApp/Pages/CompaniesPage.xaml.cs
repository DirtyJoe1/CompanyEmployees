using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
            FillIdsListBox();
        }

        private async void GetAllCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            InfoTextBox.Text = "";
            InfoTextBox.Visibility = Visibility.Visible;
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
        private async void FillIdsListBox()
        {
            CompanyIdsListBox.Items.Clear();
            var response = await _repository.GetCompaniesAsync();
            foreach (var companies in response)
            {
                CompanyIdsListBox.Items.Add(companies.Id.ToString());
            }
        }

        private async void GetCompaniesColection_Click(object sender, RoutedEventArgs e)
        {
            InfoTextBox.Text = "";
            var response = await _repository.GetCompanyCollectionAsync(CompanyIdsListBox.SelectedItems.ToDynamicList<string>());
            foreach (var companies in response)
            {
                InfoTextBox.Text += "---------------------------------------------------" + "\n";
                InfoTextBox.Text += companies.Id.ToString() + "\n";
                InfoTextBox.Text += companies.Name + "\n";
                InfoTextBox.Text += companies.Address.ToString() + "\n";
                InfoTextBox.Text += companies.Country.ToString() + "\n";
            }
        }

        private async void DeleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            await _repository.DeleteCompanyAsync(CompanyIdsListBox.SelectedItem.ToString());
            FillIdsListBox();
        }

        private async void CreateCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeForCreation = new List<EmployeeForCreationDto>
            {
                new EmployeeForCreationDto
                {
                    Name = EmployeeNameTextBox.Text,
                    Age = int.Parse(EmployeeAgeTextBox.Text),
                    Position = EmployeePositionTextBox.Text,
                }
            };
            var companyForCreation = new CompanyForCreationDto
            {
                Name = CompanyNameTextBox.Text,
                Address = CompanyAddressTextBox.Text,
                Country = CompanyCountryTextBox.Text,
                Employees = employeeForCreation,
            };
            await _repository.PostCompanyAsync(companyForCreation);
            FillIdsListBox();
        }
    }
}
