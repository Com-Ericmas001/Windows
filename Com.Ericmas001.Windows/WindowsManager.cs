using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Com.Ericmas001.Windows
{
    public static class WindowsManager
    {
        //Usage:
        //WindowsManager.ShowDialogSTA<MyWindow,MyViewModel>();
        //WindowsManager.ShowDialogSTA<MyWindow>(new MyViewModel(string parm1, bool parm2));
        public static BaseViewModel ShowDialog<TWindow>(BaseViewModel vm)
            where TWindow : Window, new()
        {
            var window = new TWindow { DataContext = vm };
            vm.OnRequestClose += (s, e) => window.Close();
            window.ShowDialog();
            return vm;
        }
        public static TViewModel ShowDialog<TWindow, TViewModel>()
            where TViewModel : BaseViewModel, new()
            where TWindow : Window, new()
        {
            return (TViewModel)ShowDialog<TWindow>(new TViewModel());
        }
        public static BaseViewModel ShowDialogSTA<TWindow>(BaseViewModel vm)
            where TWindow : Window, new()
        {
            Thread viewerThread = new Thread(delegate() { vm = ShowDialog<TWindow>(vm); });
            viewerThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            viewerThread.Start();
            viewerThread.Join();
            return vm;
        }
        public static TViewModel ShowDialogSTA<TWindow, TViewModel>()
            where TViewModel : BaseViewModel, new()
            where TWindow : Window, new()
        {
            return (TViewModel)ShowDialogSTA<TWindow>(new TViewModel());
        }
    }
}
