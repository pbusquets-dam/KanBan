using System;
using System.ComponentModel;

public class cResponsable : INotifyPropertyChanged
{
    public string usuari;

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public string Nom
    {
        get { return usuari; }
        set
        {
            usuari = value;
            OnPropertyChanged(nameof(usuari));
        }
    }

    public cResponsable(string nom)
    {
        Nom = nom;
    }

    public override string ToString()
    {
        return Nom;
    }
}
