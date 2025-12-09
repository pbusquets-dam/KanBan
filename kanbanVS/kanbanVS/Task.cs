using System;
using System.ComponentModel;


public class Task : INotifyPropertyChanged
{
    public enum State
    {
        ToDo,
        Doing,
        Done

    }
    public string text;
    public cResponsable assignedTo;
    public DateTime startDate;
    public DateTime endDate;
    public string priorityColor;
    private State status;

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

    public State Status
    {
        get { return status; }
        set
        {
            if (status != value) 
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }
    public Task(string textinicial)
    {
        Text = textinicial;
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(7);
        Status = Task.State.ToDo;
    }
}
