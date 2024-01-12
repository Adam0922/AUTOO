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
using MySql.Data.MySqlClient;

namespace JozsiAuto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            string mysqlCon = "server=localhost;user=kamiauto;database=jozsiauto;password=123;";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);

            string username = Usertxtbox.Text.ToString();
            string password = pswbox.Password.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Ne legyel üres mező");
                return;
            }

            try
            {
                mySqlConnection.Open();

                MySqlCommand mySqlCommand = new MySqlCommand("select * from users", mySqlConnection);
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (username.Equals(reader.GetString("username")) & password.Equals(reader.GetString("password")))
                        {
                            MessageBox.Show("Siker");
                        }
                        else
                        {
                            MessageBox.Show("Valami nem joe");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                mySqlConnection.Close(); 
            }
        }

        private void Usertxtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Usertxtbox.IsFocused && Usertxtbox.Text == "Username")
                Usertxtbox.Text = "";
        }

        private void Usertxtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Usertxtbox.Text != "Username" && Usertxtbox.Text == "")
                Usertxtbox.Text = "Username";

        }

        private void pswtext_GotFocus(object sender, RoutedEventArgs e)
        {
            pswtext.Visibility = Visibility.Hidden;
            pswbox.Visibility = Visibility.Visible;
            pswbox.Focus();

        }

        private void pswbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pswbox.Password == "")
            {
                pswbox.Visibility = Visibility.Hidden;
                pswtext.Visibility = Visibility.Visible;
                if (pswtext.Text != "Password")
                    pswtext.Text = "Password";

            }
        }



        //private void loginbtn_Click(object sender, RoutedEventArgs e)
        //{
        //    string mysqlCon = " server=localhost; user=kamiauto; database=jozsiauto; password=123;";
        //    MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);


        //    string username = Usertxtbox.Text.ToString();
        //    string password = pswbox.Password.ToString();
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //    {
        //        MessageBox.Show("Ne legyel üres mező");
        //    }
        //    else
        //    {
        //        MySqlCommand mySqlCommand = new MySqlCommand("select * from users", mySqlConnection);
        //        MySqlDataReader reader = mySqlCommand.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            if (username.Equals(reader.GetString("username")) && password.Equals(reader.GetString("password")))
        //            {
        //                MessageBox.Show("Siker");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Valami nem joe");
        //            }
        //        }


        //    }
        //}
    }
}
