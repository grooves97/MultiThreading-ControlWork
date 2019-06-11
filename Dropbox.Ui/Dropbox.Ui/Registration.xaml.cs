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
using System.Windows.Shapes;
using Dropbox.Models;
using Dropbox.DataAcces;
using Dropbox.Services;
using System.Data.Entity;

namespace Dropbox.Ui
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;
            var confirmedPassword = confirmedPasswordBox.Password;

            var loginLengt = 5;
            var passwordLengt = 8;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmedPassword))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }
            else if (login.Length < loginLengt)
            {
                MessageBox.Show("Логин должен содержать не меньше 6 символов!");
                return;
            }
            else if (password != confirmedPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            else if (password.Length < passwordLengt)
            {
                MessageBox.Show("Пароль должен содержать не меньше 8 символов!");
                return;
            }

            using (var context = new DataContext())
            {
                var user = await context.Users.SingleOrDefaultAsync(searchingUser => searchingUser.Login == login);

                if (user != null)
                {
                    MessageBox.Show("Такой пользователь уже существует!");
                    return;
                }
                else
                {
                    context.Users.Add(new User
                    {
                        Login = login,
                        Password = await EncryptionService.HashPassword(password)
                    });
                    context.SaveChanges();

                    MessageBox.Show("Успешно добавлено!");

                    MainWindow main = new MainWindow();
                    main.Show();

                    Close();

                }
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            registration.Close();
        }
    }
}
