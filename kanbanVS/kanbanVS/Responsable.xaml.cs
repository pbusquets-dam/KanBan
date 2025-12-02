using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace kanbanVS
{
    /// <summary>
    /// Lógica de interacción para Responsable.xaml
    /// </summary>
    public partial class Responsable : Window
    {
        public string ResponsableNom { get; private set; }
        public string ResponsableCognom { get; private set; }



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

