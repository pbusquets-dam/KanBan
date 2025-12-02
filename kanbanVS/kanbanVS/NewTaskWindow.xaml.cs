// NewTaskWindow.xaml.cs
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace kanbanVS
{
    public partial class NewTaskWindow : Window
    {
<<<<<<< HEAD
        public string NewTaskNameText { get; private set; }
        public string NewTaskDescText { get; private set; }
=======
        public string NewTaskText { get; set; }
>>>>>>> 9c19b311a084ce5f486413fdf8ad28ba12d9eaf9

        public NewTaskWindow()
        {
            InitializeComponent();
            //Fer que automaticament tinguis seleccionada la nova finestra
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