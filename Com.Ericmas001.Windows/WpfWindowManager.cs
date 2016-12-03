using System.Windows;

namespace Com.Ericmas001.Windows
{
    public static class WpfWindowManager
    {
        public static T Show<T>(this Window window) where T : BaseViewModel
        {
            T vm = window.DataContext as T;
            vm.OnRequestClose += (s, e) => window.Close();
            window.Show();
            return vm;
        }
        public static T ShowDialog<T>(this Window window) where T : BaseViewModel
        {
            T vm = window.DataContext as T;
            vm.OnRequestClose += (s, e) => window.Close();
            window.ShowDialog();
            return vm;
        }
    }
}
