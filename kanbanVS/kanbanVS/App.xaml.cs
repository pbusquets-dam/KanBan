using System.Windows;

namespace kanbanVS
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginWindow login = new LoginWindow();
            bool? result = login.ShowDialog();

            if (result == true && login.IsAdmin)
            {
                MainWindow main = new MainWindow(true);
                Application.Current.MainWindow = main;
                main.Show();
            }
            else if (result == true && !login.IsAdmin)
            {
                MainWindow main = new MainWindow(false);
                Application.Current.MainWindow = main;
                main.Show();
                
            }
            else
            {
                Shutdown();
            }
        }
    }
}
