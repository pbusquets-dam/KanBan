using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace kanbanVS
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> ToDoTasks { get; set; }
        public ObservableCollection<Task> InProgressTasks { get; set; }
        public ObservableCollection<Task> DoneTasks { get; set; }
        public ObservableCollection<cResponsable> Responsables { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ToDoTasks = new ObservableCollection<Task>();
            InProgressTasks = new ObservableCollection<Task>();
            DoneTasks = new ObservableCollection<Task>();
            Responsables = new ObservableCollection<cResponsable>();
            this.DataContext = this;
        }
        //OBRIR TASCA
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow newTaskWindow = new NewTaskWindow(Responsables);
            bool? result = newTaskWindow.ShowDialog();

            if (result == true)
            {
                string taskName = newTaskWindow.NewTaskNameText;

                Task newTask = new Task(taskName)
                {
                    AssignedTo = newTaskWindow.NewTaskResponsable,
                    PriorityColor = newTaskWindow.Color,
                    startDate = newTaskWindow.DataInici,
                    endDate = newTaskWindow.DataFi
                };

                ToDoTasks.Add(newTask);
            }
        }
        //OBRIR RESPONSABLE
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Responsable Responsable = new Responsable();
            bool? result = Responsable.ShowDialog();

            if (result == true && !string.IsNullOrEmpty(Responsable.ResponsableNom))
            {
                string respNom = Responsable.ResponsableNom;
                string respCognom = Responsable.ResponsableCognom;
                Console.WriteLine(respNom);
                cResponsable _responsable = new cResponsable(respNom, respCognom);
                Responsables.Add(_responsable);
            }
        }
        //DRAGDROP
        private void TaskBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Border border = sender as Border;
                Task task = border.Tag as Task;

                if (task != null)
                {
                    DataObject data = new DataObject("kanbanTask", task);
                    DragDrop.DoDragDrop(border, data, DragDropEffects.Move);
                }
            }
        }
        //DRAGDROP
        private void TasksColumn_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("kanbanTask"))
                e.Effects = DragDropEffects.Move;
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }
        //DRAGDROP

        private void TasksColumn_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("kanbanTask"))
            {
                Task task = e.Data.GetData("kanbanTask") as Task;

                if (task != null)
                {
                    ToDoTasks.Remove(task);
                    InProgressTasks.Remove(task);
                    DoneTasks.Remove(task);

                    ScrollViewer sv = sender as ScrollViewer;
                    if (sv == ToDoScrollViewer)
                        ToDoTasks.Add(task);
                    else if (sv == InProgressScrollViewer)
                        InProgressTasks.Add(task);
                    else if (sv == DoneScrollViewer)
                        DoneTasks.Add(task);
                }
            }
        }

        private void TaskBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                Border border = sender as Border;
                Task task = border.Tag as Task;

                if (task != null)
                {
                    EditTask(task);
                }
            }
        }

        private void EditTask(Task task)
        {
            NewTaskWindow editWindow = new NewTaskWindow(Responsables);
            editWindow.Eliminar.Visibility = Visibility.Visible;
            editWindow.TaskTextBox.Text = task.Text;
            editWindow.ResponsablesComboBox.SelectedItem = task.AssignedTo;

            editWindow.PrioritatAlta.IsChecked = task.PriorityColor == "Red";
            editWindow.PrioritatMitja.IsChecked = task.PriorityColor == "Yellow";
            editWindow.PrioritatBaixa.IsChecked = task.PriorityColor == "Green";

            editWindow.DataIniciDatePicker.SelectedDate = task.StartDate;
            editWindow.DataLimitDatePicker.SelectedDate = task.EndDate;

            bool? result = editWindow.ShowDialog();

            if (editWindow.IsDeleted)
            {
                ToDoTasks.Remove(task);
                InProgressTasks.Remove(task);
                DoneTasks.Remove(task);
            }

            if (result == true)
            {
                task.Text = editWindow.NewTaskNameText;
                task.AssignedTo = editWindow.NewTaskResponsable;
                task.PriorityColor = editWindow.Color;
                task.StartDate = editWindow.DataInici;
                task.EndDate = editWindow.DataFi;
            }
        }

    }
}
