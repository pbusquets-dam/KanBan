// NewTaskWindow.xaml.cs
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace kanbanVS
{
    public partial class NewTaskWindow : Window
    {

        public string NewTaskNameText { get; private set; }
        public cResponsable NewTaskResponsable { get; private set; }

        public string NewTaskText { get; set; }

        public string Color { get; set; }
        public DateTime DataInici { get; set; }
        public DateTime DataFi { get; set; }
        public bool IsDeleted { get; private set; } = false;

        public ObservableCollection<cResponsable> responsable { get; set; }
        public NewTaskWindow(ObservableCollection<cResponsable> responsables)
        {
            InitializeComponent();
            this.Loaded += (sender, e) => TaskTextBox.Focus();
            responsable = responsables;
            this.DataContext = this;
        }
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskNameText = TaskTextBox.Text;
            NewTaskResponsable = ResponsablesComboBox.SelectedItem as cResponsable;
            this.DialogResult = true;
            if(PrioritatAlta.IsChecked == true)
            {
                Color = "Red";
            }
            else if(PrioritatMitja.IsChecked == true)
            {
                Color = "Yellow";
            }
            else if(PrioritatBaixa.IsChecked == true)
            {
                Color = "Green";
            }
            else
            {
                Color = "Black";
            }
            if (DataIniciDatePicker.SelectedDate.HasValue)
            {
                DataInici = DataIniciDatePicker.SelectedDate.Value.Date;
            }
            else
            {
                DataInici = DateTime.Today.Date;
            }
            if (DataLimitDatePicker.SelectedDate.HasValue)
            {
                DataFi = DataLimitDatePicker.SelectedDate.Value.Date;
            }
            else
            {
                DataFi = DateTime.Today.Date.AddDays(7);
            }

            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

            IsDeleted = true;
            Close();
        }
    }
}