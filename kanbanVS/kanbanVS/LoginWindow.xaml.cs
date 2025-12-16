using System;
using System.Windows;

namespace kanbanVS
{
    public partial class LoginWindow : Window
    {
        public bool IsAdmin { get; private set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            string user = UserTextBox.Text;
            string pass = PassBox.Password;

            if (user == "admin" && pass == "admin")
            {
                this.DialogResult = true;
                IsAdmin = true;
                Close();
            }
            else if (user == "user" && pass == "user")
            {
                this.DialogResult = true;
                IsAdmin = false;
                Close();
            }
            else
            {
                MessageBox.Show("Usuari o contrasenya incorrectes", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
