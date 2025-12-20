using kanbanVS.APIClient;
using System;
using System.Configuration;
using System.Net.Http;
using System.Windows;

namespace kanbanVS
{
    public partial class LoginWindow : Window
    {
        private readonly UsersApiClient _apiClient = new UsersApiClient();
        public bool IsAdmin { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // agafem usuaris base dades
                var usuarisApi = await _apiClient.GetAllUsersAsync();
                bool trobat = false;

                foreach (var u in usuarisApi)
                {
                    // mirem si user i pass coincideixen
                    if (u.Usuari == UserTextBox.Text && u.Contrasenya == PassBox.Password)
                    {
                        if (u.EsAdmin == true)
                            {
                                IsAdmin = true;
                            }
                            trobat = true;
                    }
                }

                if (trobat)
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Usuari o contrasenya incorrectes", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de connexió: " + ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}