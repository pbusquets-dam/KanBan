using System;
using System.ComponentModel;

public class Task : INotifyPropertyChanged
{
    public string text;
    public cResponsable assignedTo;
    public DateTime startDate;
    public DateTime endDate;
    public string priorityColor;

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

    public cResponsable AssignedTo
    {
        get { return assignedTo; }
        set
        {
            assignedTo = value;
            OnPropertyChanged(nameof(AssignedTo));
        }
    }

    public DateTime StartDate
    {
        get { return startDate; }
        set
        {
            startDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }

    public DateTime EndDate
    {
        get { return endDate; }
        set
        {
            endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    public string PriorityColor
    {
        get { return priorityColor; }
        set
        {
            priorityColor = value;
            OnPropertyChanged(nameof(PriorityColor));
        }
    }

    public Task(string textinicial)
    {
        Text = textinicial;
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(7);
    }
}
