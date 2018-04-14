using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shell;

namespace Com.Ericmas001.Windows
{
    public static class AppWindow
    {
        public static IAppWindowViewModel Instance { get; set; }
        public static bool ShouldWindowBlink
        {
            set
            {
                bool shouldBlink = value && !Instance.IsWindowActive;
                Instance.ProgressState = shouldBlink ? TaskbarItemProgressState.Normal : TaskbarItemProgressState.None;
                Instance.ProgressValue = shouldBlink ? 100 : 0;
                Instance.RaisePropertyChanged(nameof(Instance.ProgressState));
                Instance.RaisePropertyChanged(nameof(Instance.ProgressValue));
            }
        }
    }
}
