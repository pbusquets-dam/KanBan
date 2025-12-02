using System;
using System.ComponentModel;

public class cResponsable : INotifyPropertyChanged
{
    public string nom;
    public string cognom;

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
        get { return nom; }
        set
        {
            nom = value;
            OnPropertyChanged(nameof(nom));
        }
    }
    public string Cognom
    {
        get { return cognom; }
        set
        {
            cognom = value;
            OnPropertyChanged(nameof(cognom));
        }
    }

    public cResponsable(string nom, string cognom)
    {
        Nom = nom;
        Cognom = cognom;
    }
}
