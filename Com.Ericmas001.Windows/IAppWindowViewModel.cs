using System.Windows.Shell;

namespace Com.Ericmas001.Windows
{
    public interface IAppWindowViewModel
    {
        bool IsWindowActive { get; set; }
        TaskbarItemProgressState ProgressState { get; set; }
        int ProgressValue { get; set; }
        void RaisePropertyChanged(string propertyName = null);

    }
}
