using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kanbanVS
{
    public class Task : INotifyPropertyChanged
    {
        public bool marcat;
        public string text;
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Marcat
        {
            get { return marcat; }
            set
            {
                marcat = value;
                OnPropertyChanged(nameof(Marcat));
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public Task(string textinicial)
        {
            Text = textinicial;
            Marcat = false;
        }
    }
}
