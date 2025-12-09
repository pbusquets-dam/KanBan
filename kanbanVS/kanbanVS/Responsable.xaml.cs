using System.Windows;

namespace kanbanVS
{
    /// <summary>
    /// Lógica de interacción para Responsable.xaml
    /// </summary>
    public partial class Responsable : Window
    {
        public string ResponsableNom { get; set; }
        public string ResponsableCognom { get; set; }



        public Responsable()
        {
            InitializeComponent();
            this.Loaded += (sender, e) => ResponsableTextBox.Focus();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            ResponsableNom = ResponsableTextBox.Text;
            ResponsableCognom = DescriptionTextBox.Text;
            this.DialogResult = true;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

