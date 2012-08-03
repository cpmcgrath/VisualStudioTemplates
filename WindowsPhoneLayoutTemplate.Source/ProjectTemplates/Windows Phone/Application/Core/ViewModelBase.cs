using System;
using System.Net;
using System.ComponentModel;

namespace $safeprojectname$.Core
{
    public class ViewModelBase : BasicBase
    {
        public App CurrentApp
        {
            get { return App.Current; }
        }
    }
}
