﻿using Entities.DataTransferObjects;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var repository = new Repository();
            var login = new UserForAuthenticationDto
            {
                UserName = UsernameTextBox.Text,
                Password = PasswordTextBox.Text
            };
            HttpResponseMessage response = await repository.PostAuthenticationLogin(login);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Успех");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
