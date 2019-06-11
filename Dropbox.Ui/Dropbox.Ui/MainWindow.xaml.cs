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
using Dropbox.Models;
using Dropbox.DataAcces;
using Dropbox.Services;
using DevOne.Security.Cryptography.BCrypt;
using System.Data.Entity;

namespace Dropbox.Ui
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

        private async void SignInButtonClick(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Норм введи");
                return;
            }

            using (var context = new DataContext())
            {
                var user = await context.Users.SingleOrDefaultAsync(u => u.Login == login);

                if (user == null || await EncryptionService.VerifyPassword(password, user.Password))
                {
                    MessageBox.Show("Неверен логин или пароль");
                    return;
                }
                else
                {
                    MessageBox.Show("Успешный вход");

                    AppWindow appWindow = new AppWindow();
                    appWindow.Show();
                }
            }

        }

        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
            Hide();
        }
    }
}
