// NewTaskWindow.xaml.cs
using System.Windows;

namespace kanbanVS
{
    public partial class NewTaskWindow : Window
    {
        public string NewTaskNameText { get; private set; }
        public string NewTaskDescText { get; private set; }

        public NewTaskWindow()
        {
            InitializeComponent();
            this.Loaded += (sender, e) => TaskTextBox.Focus();
            this.Loaded += (sender, e) => DescriptionTextBox.Focus();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskNameText = TaskTextBox.Text;
            NewTaskDescText = DescriptionTextBox.Text;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}