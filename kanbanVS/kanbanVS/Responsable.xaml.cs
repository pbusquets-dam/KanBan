using System.Windows;

namespace kanbanVS
{
    /// <summary>
    /// Lógica de interacción para Responsable.xaml
    /// </summary>
    public partial class Responsable : Window
    {
        public string ResponsableUsuari { get; set; }
        public string ResponsableContrasenya { get; set; }



        public Responsable()
        {
            InitializeComponent();
            this.Loaded += (sender, e) => ResponsableTextBox.Focus();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            ResponsableUsuari = ResponsableTextBox.Text;
            ResponsableContrasenya = ResponsablePassword.Text;
            this.DialogResult = true;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

