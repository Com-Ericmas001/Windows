using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Com.Ericmas001.Windows.Attributes.Validations;
using Com.Ericmas001.Windows.ViewCommands;

namespace Com.Ericmas001.Windows
{
    public class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo, IViewCommandSource
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event EventHandler OnRequestClose = delegate { };
        public ViewCommandManager ViewCommandManager { get; } = new ViewCommandManager();

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void CloseView()
        {
            OnRequestClose(this, new EventArgs());
        }

        public string this[string propertyName]
        {
            get
            {
                var errors = ValidationErrors(propertyName).ToArray();
                var prop = GetType().GetProperty(propertyName);
                var simples = prop.GetCustomAttributes(true).OfType<SimpleValidationAttribute>().Select(att => att.Validate(prop.GetValue(this, null) as string));
                var customs = prop.GetCustomAttributes(true).OfType<CustomValidationAttribute>().Select(att => att.Validate(this, prop.GetValue(this, null) as string));

                var allErrors = errors.Concat(simples).Concat(customs).ToArray();
                return !allErrors.Any() ? null : string.Join(Environment.NewLine, allErrors);
            }
        }

        public virtual bool EverythingValidates()
        {
            var subModels = GetType().GetProperties()
                .Where(pi => typeof(BaseViewModel).IsAssignableFrom(pi.PropertyType) && pi.GetValue(this, null) != null)
                .Select(pi => ((BaseViewModel)pi.GetValue(this, null)).EverythingValidates());

            return GetType().GetProperties().All(x => string.IsNullOrEmpty(this[x.Name]) && subModels.All(b => b));
        }

        public virtual IEnumerable<string> ValidationErrors(string propertyName)
        {
            return new string[0];
        }

        public string Error { get { return null; } }

        private RelayCommand<string> m_CopyCommand;
        public ICommand CopyCommand { get { return m_CopyCommand ?? (m_CopyCommand = new RelayCommand<string>(p => CopyToClipboard(p), p => !String.IsNullOrWhiteSpace(p))); } }

        private static void CopyToClipboard(string str)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Clipboard.SetText(str);
                    return;
                }
                catch { }
                Thread.Sleep(100);
            }
        }
    }
}
