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
using MySql.Data.MySqlClient;

namespace JozsiAuto
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string mysqlCon = "server=localhost;user=kamiauto;database=jozsiauto;password=123;";

        public Window1()
        {
            InitializeComponent();
        }

        private void registerbtn_Click(object sender, RoutedEventArgs e)
        {
            // Perform registration logic here
            string username = UsernameTextBox.Text;
            string password = PasswordBoxx.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please re-enter.");
                return;
            }

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon))
                {
                    mySqlConnection.Open();

                    // Check if the username already exists
                    string checkUsernameQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    using (MySqlCommand checkUsernameCommand = new MySqlCommand(checkUsernameQuery, mySqlConnection))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@username", username);
                        int existingUserCount = Convert.ToInt32(checkUsernameCommand.ExecuteScalar());

                        if (existingUserCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different one.");
                            return;
                        }
                    }

                    // Insert new user into the database
                    string insertUserQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    using (MySqlCommand insertUserCommand = new MySqlCommand(insertUserQuery, mySqlConnection))
                    {
                        insertUserCommand.Parameters.AddWithValue("@username", username);
                        insertUserCommand.Parameters.AddWithValue("@password", password);

                        int rowsAffected = insertUserCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration successful!");
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Registration failed. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Backgomb_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

            /*
            private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
            {
                pswtext.Visibility = Visibility.Hidden;
            }

            private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                if (UsernameTextBox.IsFocused && UsernameTextBox.Text == "Username")
                    UsernameTextBox.Text = "";
            }

            private void pswtext_GotFocus(object sender, RoutedEventArgs e)
            {
                pswtext.Visibility = Visibility.Hidden;
                PasswordBoxx.Visibility = Visibility.Visible;
                PasswordBoxx.Focus();

            }
            private void PasswordBoxx_LostFocus(object sender, RoutedEventArgs e)
            {
                if (PasswordBoxx.Password == "")
                {
                    PasswordBoxx.Visibility = Visibility.Hidden;
                    pswtext.Visibility = Visibility.Visible;
                    if (pswtext.Text != "Password")
                        pswtext.Text = "Password";

                }
            }

            private void pswtext2_GotFocus(object sender, RoutedEventArgs e)
            {
                pswtext2.Visibility = Visibility.Hidden;
                ConfirmPasswordBox.Visibility = Visibility.Visible;
                ConfirmPasswordBox.Focus();

            }
            private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
            {
                if (ConfirmPasswordBox.Password == "")
                {
                    ConfirmPasswordBox.Visibility = Visibility.Hidden;
                    pswtext2.Visibility = Visibility.Visible;
                    if (pswtext2.Text != "Password")
                        pswtext2.Text = "Password";

                }
            }

            private void UsernameTextBox_LostFocus_1(object sender, RoutedEventArgs e)
            {
                if (UsernameTextBox.Text != "Username" && UsernameTextBox.Text == "")
                    UsernameTextBox.Text = "Username";
            }
            */
        

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.IsFocused && UsernameTextBox.Text == "Username")
                UsernameTextBox.Text = "";
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text != "Username" && UsernameTextBox.Text == "")
                UsernameTextBox.Text = "Username";
        }

        private void pswtext_GotFocus(object sender, RoutedEventArgs e)
        {
            pswtext.Visibility = Visibility.Hidden;
            PasswordBoxx.Visibility = Visibility.Visible;
            PasswordBoxx.Focus();
        }

        private void PasswordBoxx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxx.Password == "")
            {
                PasswordBoxx.Visibility = Visibility.Hidden;
                pswtext.Visibility = Visibility.Visible;
                if (pswtext.Text != "Password")
                    pswtext.Text = "Password";

            }
        }

        private void pswtext2_GotFocus(object sender, RoutedEventArgs e)
        {
            pswtext2.Visibility = Visibility.Hidden;
            ConfirmPasswordBox.Visibility = Visibility.Visible;
            ConfirmPasswordBox.Focus();
        }

        private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordBox.Password == "")
            {
                ConfirmPasswordBox.Visibility = Visibility.Hidden;
                pswtext2.Visibility = Visibility.Visible;
                if (pswtext2.Text != "Repeat Password")
                    pswtext2.Text = "Repeat Password";

            }
        }
    }
}

