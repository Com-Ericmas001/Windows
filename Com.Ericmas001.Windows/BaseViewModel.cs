﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Com.Ericmas001.Windows.Validations;
using Com.Ericmas001.Windows.ViewCommands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Com.Ericmas001.Windows
{
    public class BaseViewModel : ViewModelBase, IDataErrorInfo, IViewCommandSource
    {
        public event EventHandler OnRequestClose = delegate { };

        public virtual string Error => null;


        private ViewCommandManager viewCommandManager = new ViewCommandManager();

        /// <summary>
        /// Gets the ViewCommandManager instance.
        /// </summary>
        public ViewCommandManager ViewCommandManager { get { return viewCommandManager; } }

        public string this[string propertyName]
        {
            get
            {
                PropertyInfo prop = GetType().GetProperty(propertyName);
                IEnumerable<string> simples = prop.GetCustomAttributes(true).OfType<SimpleValidationAttribute>().Select(att => att.Validate(prop.GetValue(this, null) as String));
                IEnumerable<string> customs = prop.GetCustomAttributes(true).OfType<CustomValidationAttribute>().Select(att => att.Validate(this, prop.GetValue(this, null) as String));

                return simples.Union(customs).FirstOrDefault(msg => !String.IsNullOrEmpty(msg));
            }
        }

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
                catch
                {
                    // ignored
                }
                Thread.Sleep(100);
            }
        }

        public virtual bool IsAllInputsValidated()
        {
            IEnumerable<bool> subModels = GetType()
                .GetProperties()
                .Where(pi => typeof(BaseViewModel).IsAssignableFrom(pi.PropertyType) && pi.GetValue(this, null) != null)
                .Select(pi => ((BaseViewModel)pi.GetValue(this, null)).IsAllInputsValidated());

            return GetType().GetProperties().All(x => String.IsNullOrEmpty(this[x.Name]) && subModels.All(b => b));
        }

        protected string ExecuteSimpleValidations(string value, params SimpleValidationAttribute[] validations)
        {
            return validations.Select(x => x.Validate(value)).FirstOrDefault(x => !String.IsNullOrEmpty(x));
        }

        protected void CloseView()
        {
            OnRequestClose(this, new EventArgs());
        }

        public static TViewModel ShowDialog<TViewModel, TWindow>()
            where TViewModel : BaseViewModel
            where TWindow : Window, new()
        {
            var window = new TWindow();
            var vm = window.DataContext as TViewModel;
            if (vm != null)
                vm.OnRequestClose += (s, e) => window.Close();
            window.ShowDialog();
            return vm;
        }

        public static TViewModel ShowDialogSTA<TViewModel, TWindow>()
            where TViewModel : BaseViewModel
            where TWindow : Window, new()
        {
            TViewModel vm = null;
            Thread viewerThread = new Thread(delegate() { vm = ShowDialog<TViewModel, TWindow>(); });
            viewerThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            viewerThread.Start();
            viewerThread.Join();
            return vm;
        }
    }
}
