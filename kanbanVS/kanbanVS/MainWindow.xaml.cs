using kanbanVS.APIClient;
using kanbanVS.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace kanbanVS
{
    public partial class MainWindow : Window
    {
        private readonly UsersApiClient _apiClient = new UsersApiClient();

        // Llistes per a la interfície
        public ObservableCollection<KanbanTask> ToDoTasks { get; set; } = new ObservableCollection<KanbanTask>();
        public ObservableCollection<KanbanTask> InProgressTasks { get; set; } = new ObservableCollection<KanbanTask>();
        public ObservableCollection<KanbanTask> DoneTasks { get; set; } = new ObservableCollection<KanbanTask>();
        public ObservableCollection<cResponsable> Responsables { get; set; } = new ObservableCollection<cResponsable>();

        private bool admin = false;
        public bool IsAdmin => admin;

        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            this.admin = isAdmin;
            this.DataContext = this;

            CarregarDadesInicials(); // agafem dades api
        }

        private async void CarregarDadesInicials()
        {
            try
            {
                // Carreguem els responsables
                var usersApi = await _apiClient.GetAllUsersAsync();
                foreach (var user in usersApi)
                {
                    Responsables.Add(new cResponsable(user.Usuari) { Id = (int)user.Id });
                }

                // Carreguem les tasques i les posem a la seva columna
                var tasquesApi = await _apiClient.GetAllTascaAsync();
                foreach (var t in tasquesApi)
                {
                    KanbanTask kt = new KanbanTask(t.Titol)
                    {
                        Codi = t.Codi,
                        PriorityColor = t.Prioritat,
                        StartDate = t.DataInici,
                        EndDate = t.DataFinal
                    };

                    // Busquem qui és el responsable d'aquesta tasca
                    foreach (var r in Responsables)
                    {
                        if (r.Id == t.IdResp) { kt.AssignedTo = r; break; }
                    }

                    if (t.Estat == "ToDo") ToDoTasks.Add(kt);
                    else if (t.Estat == "Doing") InProgressTasks.Add(kt);
                    else if (t.Estat == "Done") DoneTasks.Add(kt);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        // Crear una tasca nova
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            NewTaskWindow win = new NewTaskWindow(Responsables);
            if (win.ShowDialog() == true)
            {
                Tasca tascaApi = new Tasca
                {
                    Titol = win.NewTaskNameText,
                    DataInici = win.DataInici,
                    DataFinal = win.DataFi,
                    Prioritat = win.Color,
                    Estat = "ToDo",
                    IdResp = win.NewTaskResponsable?.Id ?? 1
                };

                await _apiClient.AddTascaAsync(tascaApi); // Enviem a la base de dades

                // Netejem i tornem a carregar per refrescar IDs
                ToDoTasks.Clear(); InProgressTasks.Clear(); DoneTasks.Clear();
                CarregarDadesInicials();
            }
        }

        // Crear un responsable nou
        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            Responsable window = new Responsable();
            if (window.ShowDialog() == true)
            {
                var userApi = new Model.User
                {
                    Usuari = window.ResponsableUsuari,
                    Contrasenya = window.ResponsableContrasenya
                };

                await _apiClient.AddUserAsync(userApi);

                Responsables.Clear(); // Refresquem la llista de la UI
                CarregarDadesInicials();
            }
        }

        private void TaskBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Border border = sender as Border;
                KanbanTask task = border?.Tag as KanbanTask;
                if (task != null)
                {
                    DataObject data = new DataObject("kanbanTask", task);
                    DragDrop.DoDragDrop(border, data, DragDropEffects.Move);
                }
            }
        }

        private void TasksColumn_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent("kanbanTask") ? DragDropEffects.Move : DragDropEffects.None;
            e.Handled = true;
        }

        // Quan deixem anar la tasca en una columna (Update API)
        private async void TasksColumn_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("kanbanTask"))
            {
                KanbanTask task = e.Data.GetData("kanbanTask") as KanbanTask;
                if (task != null)
                {
                    string estat = "";
                    ToDoTasks.Remove(task);
                    InProgressTasks.Remove(task);
                    DoneTasks.Remove(task);

                    ScrollViewer sv = sender as ScrollViewer;
                    if (sv == ToDoScrollViewer) { ToDoTasks.Add(task); task.Status = KanbanTask.State.ToDo; estat = "ToDo"; }
                    else if (sv == InProgressScrollViewer) { InProgressTasks.Add(task); task.Status = KanbanTask.State.Doing; estat = "Doing"; }
                    else if (sv == DoneScrollViewer) { DoneTasks.Add(task); task.Status = KanbanTask.State.Done; estat = "Done"; }

                    try
                    {
                        // Actualitzem l'estat a la base de dades
                        Tasca tascaUpdate = new Tasca
                        {
                            Codi = task.Codi,
                            Titol = task.Text,
                            Estat = estat,
                            Prioritat = task.PriorityColor,
                            DataInici = task.StartDate,
                            DataFinal = task.EndDate,
                            IdResp = task.AssignedTo?.Id ?? 1
                        };
                        await _apiClient.UpdateTascaAsync(tascaUpdate);
                    }
                    catch (Exception ex) { MessageBox.Show("Error al moure: " + ex.Message); }
                }
            }
        }

        private void TaskBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && IsAdmin)
            {
                if ((sender as Border)?.Tag is KanbanTask task) EditTask(task);
            }
        }

        // Editar o eliminar tasca existent
        private async void EditTask(KanbanTask task)
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

            if (editWindow.IsDeleted) // Opció d'eliminar
            {
                try
                {
                    await _apiClient.DeleteTascaAsync((int)task.Codi);
                    ToDoTasks.Remove(task); InProgressTasks.Remove(task); DoneTasks.Remove(task);
                }
                catch (Exception ex) { MessageBox.Show("Error eliminant: " + ex.Message); }
            }
            else if (result == true) // Opció de guardar canvis
            {
                task.Text = editWindow.NewTaskNameText;
                task.AssignedTo = editWindow.NewTaskResponsable;
                task.PriorityColor = editWindow.Color;
                task.StartDate = editWindow.DataInici;
                task.EndDate = editWindow.DataFi;

                try
                {
                    Tasca tascaEditada = new Tasca
                    {
                        Codi = task.Codi,
                        Titol = task.Text,
                        Estat = task.Status.ToString(),
                        Prioritat = task.PriorityColor,
                        DataInici = task.StartDate,
                        DataFinal = task.EndDate,
                        IdResp = task.AssignedTo?.Id ?? 1
                    };
                    await _apiClient.UpdateTascaAsync(tascaEditada);
                }
                catch (Exception ex) { MessageBox.Show("Error editant: " + ex.Message); }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosing(e);
        }
    }
}