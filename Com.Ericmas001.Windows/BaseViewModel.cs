using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Com.Ericmas001.Windows
{
    public class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event EventHandler OnRequestClose = delegate { };

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void CloseView()
        {
            OnRequestClose(this, new EventArgs());
        }

        public string this[string columnName]
        {
            get
            {
                var errors = ValidationErrors(columnName).ToArray();
                return !errors.Any() ? null : string.Join(Environment.NewLine, errors);
            }
        }

        public virtual IEnumerable<string> ValidationErrors(string propertyName)
        {
            return new string[0];
        }

        public string Error { get { return null; } }
    }
}
