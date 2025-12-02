// NewTaskWindow.xaml.cs
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace kanbanVS
{
    public partial class NewTaskWindow : Window
    {

        public string NewTaskNameText { get; private set; }
        public string NewTaskDescText { get; private set; }

        public string NewTaskText { get; set; }


        public NewTaskWindow()
        {
            InitializeComponent();
            this.Loaded += (sender, e) => TaskTextBox.Focus();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskNameText = TaskTextBox.Text;
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}