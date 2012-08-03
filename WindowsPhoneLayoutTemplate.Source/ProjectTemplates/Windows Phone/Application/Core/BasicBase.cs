using System;
using System.Net;
using System.ComponentModel;

namespace $safeprojectname$.Core
{
    public class BasicBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged  = delegate { };

        protected virtual void SendPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
