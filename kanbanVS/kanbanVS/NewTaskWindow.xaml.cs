// NewTaskWindow.xaml.cs
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace kanbanVS
{
    public partial class NewTaskWindow : Window
    {
        public string NewTaskText { get; set; }

        public NewTaskWindow()
        {
            InitializeComponent();
            //Fer que automaticament tinguis seleccionada la nova finestra
            this.Loaded += (sender, e) => TaskTextBox.Focus();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskText = TaskTextBox.Text;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}