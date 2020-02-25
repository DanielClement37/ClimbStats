using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using ClimbStats.Models;

namespace ClimbStats.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public BaseViewModel()
        {

        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
    }

    //{ 
    //    bool isBusy = false;
    //    public bool IsBusy
    //    {
    //        get { return isBusy; }
    //        set { SetProperty(ref isBusy, value); }
    //    }



    //    protected bool SetProperty<T>(ref T backingStore, T value,
    //        [CallerMemberName]string propertyName = "",
    //        Action onChanged = null)
    //    {
    //        if (EqualityComparer<T>.Default.Equals(backingStore, value))
    //            return false;

    //        backingStore = value;
    //        onChanged?.Invoke();
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }

    //    #region INotifyPropertyChanged
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    //    {
    //        var changed = PropertyChanged;
    //        if (changed == null)
    //            return;

    //        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    #endregion
}
