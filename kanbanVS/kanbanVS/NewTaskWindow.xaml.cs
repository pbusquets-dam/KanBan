// NewTaskWindow.xaml.cs
using System.Windows;

namespace kanbanVS
{
    public partial class NewTaskWindow : Window
    {
        public string NewTaskText { get; private set; }

        public NewTaskWindow()
        {
            InitializeComponent();
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