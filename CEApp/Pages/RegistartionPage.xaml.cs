using Azure;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для RegistartionPage.xaml
    /// </summary>
    public partial class RegistartionPage : Page
    {
        Repository _repository;
        public RegistartionPage(Repository repository)
        {
            InitializeComponent();
            _repository = repository;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedRoles = new List<string>
            {
                "Manager"
            };
            var register = new UserForRegistrationDto
            {
                FirstName = FirstnameTextBox.Text,
                LastName = LastnameTextBox.Text,
                UserName = UsernameTextBox.Text,
                Password = PasswordTextBox.Text,
                Email = EmailTextBox.Text,
                PhoneNumber = PhoneTextBox.Text,
                Roles = selectedRoles,
            };
            HttpResponseMessage response = await _repository.PostAuthenticationRegister(register);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Success");
                NavigationService.Navigate(new LoginPage());
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }
    }
}
