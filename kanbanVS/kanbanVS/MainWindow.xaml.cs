using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kanbanVS
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> ToDoTasks { get; set; }
        public ObservableCollection<Task> InProgressTasks { get; set; }
        public ObservableCollection<Task> DoneTasks { get; set; }

        public MainWindow()
        {

            InitializeComponent();
            ToDoTasks = new ObservableCollection<Task>();
            InProgressTasks = new ObservableCollection<Task>();
            DoneTasks = new ObservableCollection<Task>();
            ToDoTasks.Add(new Task("PUTA"));
            InProgressTasks.Add(new Task("PUTAPUTA"));
            DoneTasks.Add(new Task("PUTAPUTAPUTA"));
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow taskDialog = new NewTaskWindow();
            bool? dialogResult = taskDialog.ShowDialog();
            if (dialogResult == true)
            {
                string taskContent = taskDialog.NewTaskText;
                if (!string.IsNullOrWhiteSpace(taskContent))
                {
                    ToDoTasks.Add(new Task(taskContent));
                }
            }
        }
        private void TaskBorderClick(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var task = border.Tag as Task;

            task.Marcat = !task.Marcat;

            border.Background = task.Marcat ? Brushes.LightGray : Brushes.White;
        }

    }
}
